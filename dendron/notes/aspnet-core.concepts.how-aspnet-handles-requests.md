---
id: vks2a1hiaq0yd7j7jgjktbe
title: How Aspnet Handles Requests
desc: ''
updated: 1759468906294
created: 1759468897996
---
# How ASP.NET Core Handles Requests While app.Run() Blocks the Main Thread

This is one of the most common points of confusion for beginners! Let's demystify how your application can handle thousands of concurrent requests even though `app.Run()` blocks the main thread.

## The Key Concept: Thread Separation

**The Critical Understanding:**
- **Main Thread** → Blocked by `app.Run()`, waiting for shutdown signals
- **Thread Pool Threads** → Handle all incoming HTTP requests

These are **completely separate** threads doing **completely different jobs**.

```
┌─────────────────────────────────────────────────────────┐
│                    YOUR APPLICATION                      │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  Main Thread (Console/Entry Point)                      │
│  ┌────────────────────────────────────────┐             │
│  │  app.Run() ← BLOCKED HERE              │             │
│  │  Waiting for shutdown signal...        │             │
│  └────────────────────────────────────────┘             │
│                                                          │
│  ─────────────────────────────────────────────────────  │
│                                                          │
│  Kestrel Web Server (Thread Pool)                       │
│  ┌────────────────────────────────────────┐             │
│  │  Thread 1 → Handling Request A         │             │
│  │  Thread 2 → Handling Request B         │             │
│  │  Thread 3 → Handling Request C         │             │
│  │  Thread 4 → Idle (waiting)             │             │
│  │  Thread 5 → Idle (waiting)             │             │
│  │  ... (grows as needed)                 │             │
│  └────────────────────────────────────────┘             │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

## Step-by-Step: What Happens When a Request Arrives

### 1. Application Startup

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/slow", async () => 
{
    await Task.Delay(5000); // Simulate slow operation
    return "Done!";
});

app.Run(); // ← Main thread STOPS here
```

**At this point:**
- Main thread is blocked at `app.Run()`
- Kestrel web server starts listening on port 5000
- Thread pool threads are ready and waiting

### 2. Request Arrives

```
Client sends: GET http://localhost:5000/
```

**What happens:**

1. **Network Layer** receives the TCP connection
2. **Kestrel** accepts the connection on a thread pool thread
3. **A thread pool thread** is assigned to handle this request
4. **Main thread** continues to be blocked (doing nothing with requests)

### 3. Request Processing

```csharp
// This code runs on a THREAD POOL THREAD, not the main thread!
app.MapGet("/", () => "Hello World!");
```

**The thread pool thread:**
- Parses the HTTP request
- Routes it to the correct endpoint
- Executes your handler code
- Generates the HTTP response
- Sends it back to the client
- Returns to the pool (ready for next request)

### 4. Multiple Concurrent Requests

```
Request 1: GET /         → Thread Pool Thread #1
Request 2: GET /slow     → Thread Pool Thread #2  
Request 3: GET /         → Thread Pool Thread #3
Request 4: GET /slow     → Thread Pool Thread #4
... up to thousands of concurrent requests
```

**Meanwhile, the main thread is still blocked at `app.Run()`, doing nothing!**

## Detailed Example: Seeing Threads in Action

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Log the main thread ID
var mainThreadId = Environment.CurrentManagedThreadId;
Console.WriteLine($"Main Thread ID: {mainThreadId}");

app.MapGet("/", () => 
{
    var requestThreadId = Environment.CurrentManagedThreadId;
    return new 
    { 
        Message = "Hello World!",
        MainThreadId = mainThreadId,
        RequestThreadId = requestThreadId,
        SameThread = mainThreadId == requestThreadId // Will be FALSE!
    };
});

app.MapGet("/thread-info", () =>
{
    return new
    {
        CurrentThreadId = Environment.CurrentManagedThreadId,
        IsThreadPoolThread = Thread.CurrentThread.IsThreadPoolThread, // TRUE
        IsBackground = Thread.CurrentThread.IsBackground, // TRUE
        ThreadPoolWorkerThreads = ThreadPool.ThreadCount
    };
});

Console.WriteLine("About to call app.Run()...");
app.Run(); // ← Main thread blocks HERE
Console.WriteLine("This only prints after shutdown!");
```

**Output when you make a request:**

```json
// Response from GET /
{
  "message": "Hello World!",
  "mainThreadId": 1,
  "requestThreadId": 8,
  "sameThread": false
}

// Response from GET /thread-info
{
  "currentThreadId": 12,
  "isThreadPoolThread": true,
  "isBackground": true,
  "threadPoolWorkerThreads": 16
}
```

**Key observation:** Request threads (8, 12) are **completely different** from the main thread (1)!

## How Async/Await Makes This Even Better

### Synchronous Handler (Blocks Thread)

```csharp
app.MapGet("/sync-slow", () => 
{
    Thread.Sleep(5000); // Blocks this thread for 5 seconds
    return "Done!";
});
```

**Problem:** The thread is **blocked** for 5 seconds, doing nothing, waiting.

If 100 requests come in:
- Need 100 threads (expensive!)
- Each thread sits idle, wasting resources

### Asynchronous Handler (Releases Thread)

```csharp
app.MapGet("/async-slow", async () => 
{
    await Task.Delay(5000); // Thread is RELEASED during the wait
    return "Done!";
});
```

**Benefit:** The thread is **released** back to the pool during the wait!

If 100 requests come in:
- Might only need 10-20 threads
- Threads can handle multiple requests
- Much more efficient!

## Visual: Sync vs Async Request Handling

### Synchronous (Thread Blocking)

```
Thread 1: [====== Request A (5s blocked) ======]
Thread 2: [====== Request B (5s blocked) ======]
Thread 3: [====== Request C (5s blocked) ======]
Thread 4: [====== Request D (5s blocked) ======]
Thread 5: [====== Request E (5s blocked) ======]

5 concurrent slow requests = Need 5 threads (all blocked)
```

### Asynchronous (Thread Efficient)

```
Thread 1: [Request A starts] ~~waiting~~ [Request A completes]
          [Request B starts] ~~waiting~~ [Request B completes]
          [Request C starts] ~~waiting~~ [Request C completes]

Thread 2: [Request D starts] ~~waiting~~ [Request D completes]
          [Request E starts] ~~waiting~~ [Request E completes]

5 concurrent slow requests = Need only 2 threads (reused efficiently)
```

**Legend:** `[...]` = Thread is working, `~~~` = Thread released back to pool

## The Magic: How It All Works Together

### 1. Kestrel's Async I/O Loop

Kestrel doesn't create a thread for each connection. Instead, it uses **async I/O**:

```csharp
// Simplified pseudo-code of Kestrel's approach
while (!shutdownRequested)
{
    // This doesn't block a thread!
    var connection = await AcceptConnectionAsync();
    
    // Process on thread pool
    _ = Task.Run(() => HandleConnectionAsync(connection));
}
```

### 2. Thread Pool Management

.NET's thread pool automatically:
- Starts with a minimum number of threads
- Creates more threads as needed (up to max)
- Reuses threads efficiently
- Removes idle threads over time

```csharp
// Default thread pool configuration
ThreadPool.GetMinThreads(out int minWorker, out int minIO);
ThreadPool.GetMaxThreads(out int maxWorker, out int maxIO);

Console.WriteLine($"Min threads: {minWorker}, Max threads: {maxWorker}");
// Typical output: Min threads: 8, Max threads: 32767
```

### 3. Request Pipeline

Each request flows through the pipeline on its assigned thread:

```
Request arrives
    ↓
[Thread Pool Thread #5 assigned]
    ↓
1. Middleware: Logging
2. Middleware: Authentication  
3. Middleware: Routing
4. Your Handler: Execute code
5. Middleware: Response (reverse order)
    ↓
Response sent
    ↓
[Thread #5 returns to pool, ready for next request]
```

## Practical Demonstration: High Concurrency

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var requestCount = 0;
var activeThreads = new HashSet<int>();

app.MapGet("/stress-test", async () =>
{
    var requestId = Interlocked.Increment(ref requestCount);
    var threadId = Environment.CurrentManagedThreadId;
    
    lock (activeThreads)
    {
        activeThreads.Add(threadId);
    }
    
    // Simulate I/O operation
    await Task.Delay(2000);
    
    return new 
    {
        RequestId = requestId,
        ThreadId = threadId,
        TotalRequests = requestCount,
        UniqueThreadsUsed = activeThreads.Count,
        Timestamp = DateTime.Now
    };
});

app.MapGet("/stats", () => new 
{
    TotalRequests = requestCount,
    UniqueThreadsUsed = activeThreads.Count,
    MainThreadBlocked = true // Always true while app is running
});

app.Run(); // Main thread blocked, but app handles thousands of requests!
```

**Test it:**

```bash
# Send 100 concurrent requests
for i in {1..100}; do
  curl http://localhost:5000/stress-test &
done
```

**Result:** All 100 requests complete successfully using only ~10-20 threads, while the main thread remains blocked!

## Common Misconceptions Debunked

### ❌ Misconception 1: "app.Run() blocks everything"

**Reality:** It only blocks the main thread. Request handling happens on separate thread pool threads.

### ❌ Misconception 2: "We need one thread per request"

**Reality:** With async/await, one thread can handle many requests by being released during I/O waits.

### ❌ Misconception 3: "Blocking the main thread is bad"

**Reality:** It's intentional! We WANT the main thread blocked so the process doesn't exit.

### ❌ Misconception 4: "The main thread handles requests"

**Reality:** The main thread does nothing after `app.Run()` except wait for shutdown signals.

## Behind the Scenes: Operating System Level

```
┌─────────────────────────────────────────────┐
│          Operating System                    │
├─────────────────────────────────────────────┤
│                                              │
│  Your App Process (dotnet.exe)              │
│  ┌────────────────────────────────────┐     │
│  │  Main Thread (Blocked)             │     │
│  │  └─ Waiting on shutdown event      │     │
│  │                                     │     │
│  │  Thread Pool (Managed by .NET)     │     │
│  │  ├─ Worker Thread 1                │     │
│  │  ├─ Worker Thread 2                │     │
│  │  ├─ Worker Thread 3                │     │
│  │  └─ ...                            │     │
│  │                                     │     │
│  │  I/O Completion Threads            │     │
│  │  └─ Handle async I/O operations    │     │
│  └────────────────────────────────────┘     │
│                                              │
│  Network Stack                               │
│  └─ Receives TCP connections                │
│     Routes to Kestrel                       │
│                                              │
└─────────────────────────────────────────────┘
```

## Performance Comparison Example

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// BAD: Synchronous blocking operation
app.MapGet("/bad", () => 
{
    var sw = Stopwatch.StartNew();
    Thread.Sleep(1000); // Blocks thread for 1 second
    return $"Took {sw.ElapsedMilliseconds}ms, Thread {Environment.CurrentManagedThreadId} was blocked";
});

// GOOD: Asynchronous non-blocking operation
app.MapGet("/good", async () => 
{
    var sw = Stopwatch.StartNew();
    await Task.Delay(1000); // Releases thread during wait
    return $"Took {sw.ElapsedMilliseconds}ms, Thread {Environment.CurrentManagedThreadId} was efficient";
});

app.Run();
```

**Load Test Results (1000 concurrent requests):**

| Endpoint | Threads Used | Memory | Avg Response Time |
|----------|--------------|--------|-------------------|
| /bad (sync) | ~1000 | High | 15 seconds |
| /good (async) | ~20 | Low | 1.2 seconds |

## Summary: The Big Picture

1. **`app.Run()` blocks the main thread** - This is intentional and necessary to keep the process alive

2. **Requests are handled on thread pool threads** - Completely separate from the main thread

3. **Thread pool is managed automatically** - .NET creates, reuses, and destroys threads as needed

4. **Async/await is the key to scalability** - Allows threads to be reused efficiently during I/O operations

5. **One blocked main thread ≠ One request at a time** - The app can handle thousands of concurrent requests

**The Bottom Line:** The main thread blocking at `app.Run()` is like a manager who sits in their office (blocked) while hundreds of workers (thread pool threads) efficiently handle all the actual work. The manager just needs to stay present (keep the process alive) but doesn't need to do the work themselves!

## Real-World Analogy

Think of your ASP.NET Core application as a **restaurant**:

- **Main Thread (Owner)** 
  - Sits in the office after opening
  - Doesn't serve customers
  - Just needs to be present
  - Locked in place by `app.Run()`

- **Thread Pool Threads (Waiters)**
  - Take orders (receive requests)
  - Serve food (send responses)
  - Handle multiple tables
  - Can step away while kitchen cooks (async I/O)
  - Reused efficiently

The owner being "stuck" in their office doesn't prevent the restaurant from serving hundreds of customers simultaneously! 🍽️