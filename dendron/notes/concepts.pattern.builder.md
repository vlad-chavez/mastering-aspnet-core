---
id: kitmjo75kpyaicerlw94q8r
title: The Builder Pattern
desc: ''
updated: 1759462166993
created: 1759459493808
---

The Builder Pattern is a design pattern that separates the construction of a complex object from its representation. It allows you to construct objects step-by-step, giving you fine-grained control over the construction process.
Think of building a house:

❌ Bad approach: Try to build everything at once in the constructor
✅ Builder approach: Build piece by piece (foundation → walls → roof → interior)

### Simple Example
### Without Builder Pattern (Messy)

```c#
public class Car
{
    public Car(string make, string model, int year, string color, 
               bool hasSunroof, bool hasLeatherSeats, bool hasNavigation)
    {
        // Constructor with too many parameters!
        // What if you only want some features?
    }
}

// Confusing to use:
var car = new Car("Toyota", "Camry", 2024, "Blue", false, true, false);
// What does false, true, false mean? 🤔
```

### With Builder Pattern (Clean)
```c#
public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public bool HasSunroof { get; set; }
    public bool HasLeatherSeats { get; set; }
    public bool HasNavigation { get; set; }
}

public class CarBuilder
{
    private Car _car = new Car();

    public CarBuilder SetMake(string make)
    {
        _car.Make = make;
        return this;  // Return the builder for chaining
    }

    public CarBuilder SetModel(string model)
    {
        _car.Model = model;
        return this;
    }

    public CarBuilder SetYear(int year)
    {
        _car.Year = year;
        return this;
    }

    public CarBuilder SetColor(string color)
    {
        _car.Color = color;
        return this;
    }

    public CarBuilder AddSunroof()
    {
        _car.HasSunroof = true;
        return this;
    }

    public CarBuilder AddLeatherSeats()
    {
        _car.HasLeatherSeats = true;
        return this;
    }

    public CarBuilder AddNavigation()
    {
        _car.HasNavigation = true;
        return this;
    }

    public Car Build()
    {
        // Validate if needed
        if (string.IsNullOrEmpty(_car.Make))
            throw new InvalidOperationException("Car must have a make");
        
        return _car;
    }
}

// Now it's clear and readable:
var car = new CarBuilder()
    .SetMake("Toyota")
    .SetModel("Camry")
    .SetYear(2024)
    .SetColor("Blue")
    .AddLeatherSeats()
    .Build();
```

## Key Benefits
### 1. Readability
Each method call clearly states what it's doing:
```c#
.SetColor("Blue")      // Crystal clear!
vs
new Car(..., "Blue", ...)  // What parameter is this?
```

### 2. Flexibility
You only set what you need:
```c#
// Basic car
var basicCar = new CarBuilder()
    .SetMake("Honda")
    .SetModel("Civic")
    .Build();

// Luxury car
var luxuryCar = new CarBuilder()
    .SetMake("BMW")
    .SetModel("M5")
    .AddSunroof()
    .AddLeatherSeats()
    .AddNavigation()
    .Build();
```

### 3. Validation
You can validate in the Build() method:
```C#
public Car Build()
{
    if (_car.Year < 1900 || _car.Year > DateTime.Now.Year + 1)
        throw new ArgumentException("Invalid year");
    
    return _car;
}
```

### 4. Immutability
You can make the final object immutable:
```c#
public class Car
{
    public string Make { get; init; }  // init = set only during construction
    public string Model { get; init; }
    
    // Constructor is internal, only builder can create it
    internal Car() { }
}
```

## ASP.NET Core's WebApplicationBuilder
Now let's see how ASP.NET Core uses this pattern:
```c#
var builder = WebApplication.CreateBuilder(args);

// Each method configures a different aspect
builder.Services.AddControllers();           // Configure services
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddAuthentication();

builder.Configuration.AddJsonFile("custom.json");  // Configure settings
builder.Logging.AddConsole();                      // Configure logging
builder.WebHost.UseUrls("http://localhost:5001");  // Configure web host

// Build the final application
var app = builder.Build();
```
## What's Being Built?
The builder is constructing a WebApplication with:
- A configured Dependency Injection container
- A configured Configuration system
- A configured Logging system
- A configured Web server (Kestrel)
- And more...

## Why Not Just Use a Constructor?
Imagine if there was no builder:
```c#
// This would be horrible:
var app = new WebApplication(
    services: configureServices,
    configuration: configureConfiguration,
    logging: configureLogging,
    webHost: configureWebHost,
    // ... 20 more parameters
);
```

## Fluent Interface
Notice how methods return this:
```c#
public CarBuilder SetMake(string make)
{
    _car.Make = make;
    return this;  // ← This enables chaining
}
```
This is called a Fluent Interface and allows method chaining:
```c#
builder.SetMake("Toyota")
       .SetModel("Camry")    // ← Can chain because SetMake() returns the builder
       .SetYear(2024)        // ← Can chain because SetModel() returns the builder
       .Build();
```

## Real-World Analogy
Ordering at Subway (the sandwich shop):

```C#
var sandwich = new SandwichBuilder()
    .ChooseBread("Italian")           // Step 1: Choose bread
    .AddMeat("Turkey")                // Step 2: Add meat
    .AddCheese("Provolone")           // Step 3: Add cheese
    .AddVegetables("Lettuce", "Tomato") // Step 4: Add veggies
    .AddSauce("Mayo")                 // Step 5: Add sauce
    .Toast()                          // Step 6: Toast it
    .Build();                         // Final: Get your sandwich
```

## When to Use Builder Pattern
✅ Use it when:

Object construction is complex
You have many optional parameters
You want to make construction steps clear
You need validation before creating the object
Construction requires multiple steps in a specific order

❌ Don't use it when:

Object is simple with few parameters
All parameters are required
Simple constructors work fine

## Summary
The Builder Pattern is all about making complex object construction clear, flexible, and maintainable. It separates the construction logic from the object itself, allowing you to build objects step-by-step with a clean, readable syntax.