---
id: 7k0alt6akelf1b57u0c3p0z
title: First Application
desc: ''
updated: 1759469040899
created: 1759458549061
---
# ASP.NET Core Program.cs Boilerplate Code Explanation

This is the minimal hosting model introduced in .NET 6, which greatly simplifies ASP.NET Core applications. Let's break down each line:

## The Complete Boilerplate

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

## Line-by-Line Breakdown

### `var builder = WebApplication.CreateBuilder(args);`

**What it does:**

- Creates a WebApplicationBuilder instance
- Sets up the foundation for your web application
- The args parameter passes command-line arguments to configure the app

**Behind the scenes, this automatically:**

- Configures logging (console, debug, etc.)
- Sets up configuration sources (appsettings.json, environment variables, user secrets, command-line args)
- Configures the web server (Kestrel)
- Sets up dependency injection container
- Configures the content root and web root paths

**Think of it like:** Setting up a construction site with all the tools and materials ready before building.

---

### `var app = builder.Build();`

**What it does:**

- Takes all the configuration from the builder
- Creates the actual WebApplication instance
- This is your configured web application ready to handle HTTP requests

**Think of it like:** The builder is like a blueprint, and Build() constructs the actual house from that blueprint.

**Important:** After this line, you can no longer add services to builder.Services. Configuration is locked in.

---

### `app.MapGet("/", () => "Hello World!");`

**What it does:**

- Defines a route that responds to HTTP GET requests
- When someone visits the root URL (/), it returns "Hello World!"
- Uses minimal APIs - a simple way to create endpoints without controllers

**Parameters explained:**

- `"/"` - The URL pattern (root of your website)
- `() => "Hello World!"` - A lambda function that returns the response

**Examples of other HTTP verbs:**

```csharp
app.MapGet("/hello", () => "GET request");
app.MapPost("/create", () => "POST request");
app.MapPut("/update", () => "PUT request");
app.MapDelete("/delete", () => "DELETE request");
```

---

### `app.Run();`

**What it does:**

- Starts the web server
- Begins listening for incoming HTTP requests
- Blocks the main thread (keeps the application running)
- This is typically the last line in Program.cs

**To stop the application:**

- Press Ctrl+C in the terminal
- Close the terminal window
- Stop debugging in Visual Studio

## Key Concepts for Beginners

### What is Minimal Hosting Model?

This new approach (since .NET 6) eliminates the need for:

- Startup.cs file (used to configure services and middleware)
- Explicit Main method boilerplate
- Separate CreateHostBuilder method
- Lots of ceremonial code

**Before .NET 6 (the old way):**

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

// Plus a separate Startup.cs file!
```

**After .NET 6 (the new way):**

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.Run();
```

Much simpler! 🎉

### Understanding the Two Phases

#### Phase 1: Configuration (Before Build())

This is where you add services and configure settings:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddAuthentication();

// Configure settings
builder.Configuration.AddJsonFile("mysettings.json");
builder.Logging.SetMinimumLevel(LogLevel.Warning);

var app = builder.Build(); // ← Configuration phase ends here
```

**Key point:** Use builder.Services to add things to dependency injection.

#### Phase 2: Request Pipeline (After Build())

This is where you add middleware and define routes:

```csharp
var app = builder.Build();

// Configure the HTTP request pipeline (middleware)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// Define routes
app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run(); // ← Start listening for requests
```

**Key point:** Middleware order matters! Requests flow through middleware in the order you add them.

## Expanding the Boilerplate

As your application grows, you'll add more configuration:

### Example: A More Complete Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// ===== PHASE 1: CONFIGURATION =====

// Add services to DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add custom services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// ===== PHASE 2: REQUEST PIPELINE =====

// Configure middleware (order matters!)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapGet("/", () => "Hello World!");
app.MapGet("/api/users", (IUserService userService) => userService.GetAll());
app.MapControllers();

app.Run();
```

## Common Beginner Questions

### Q: What does args do?

The args parameter allows you to pass configuration via command line:

```bash
dotnet run --urls "http://localhost:5001" --environment Production
```

### Q: What's the difference between builder.Services and app.Use?

- **builder.Services** - Register services for dependency injection (before Build())
- **app.Use** - Add middleware to handle HTTP requests (after Build())

### Q: Why does app.Run() block?

It needs to keep the web server running and listening for HTTP requests. The application stays alive until you manually stop it.

### Q: Can I still use Controllers instead of Minimal APIs?

Yes! Just add:

```csharp
builder.Services.AddControllers();  // Before Build()
app.MapControllers();               // After Build()
```

Then create controller classes as usual.

### Q: What is middleware?

Middleware are components that handle HTTP requests and responses. They form a pipeline:

```
Request → Middleware 1 → Middleware 2 → Middleware 3 → Your Code
Response ← Middleware 1 ← Middleware 2 ← Middleware 3 ← Your Code
```

## Visual Flow Diagram

```c#
Command Line Arguments (args)
         ↓
WebApplication.CreateBuilder(args)
         ↓
    [Builder Phase]
    - Add Services
    - Configure Settings
    - Configure Logging
         ↓
    builder.Build()
         ↓
    [Application Phase]
    - Configure Middleware
    - Map Routes
         ↓
      app.Run()
         ↓
   [Server Running]
   Listening for HTTP requests...
```

## Quick Reference: Common Patterns

### Adding a Database

```csharp
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Adding Authentication

```csharp
builder.Services.AddAuthentication();
// ...after Build()
app.UseAuthentication();
app.UseAuthorization();
```

### Adding CORS

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
// ...after Build()
app.UseCors("AllowAll");
```

### Serving Static Files

```csharp
app.UseStaticFiles(); // Serves files from wwwroot folder
```

### Adding Swagger (API documentation)

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ...after Build()
app.UseSwagger();
app.UseSwaggerUI();
```

## Summary

The Program.cs boilerplate is your application's entry point:

1. **Create a builder** - Set up configuration
2. **Build the app** - Create the web application
3. **Configure pipeline** - Add middleware and routes
4. **Run the app** - Start listening for requests

It's simple by default but can grow to handle complex enterprise applications! 🚀

[[aspnet-core.concepts.understanding-app-run]]