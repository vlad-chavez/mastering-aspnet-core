---
id: 3frg7mouaewtz382k2425zs
title: Net CLI Guide
desc: ''
updated: 1759436010741
created: 1759435339694
---
## Initial Setup

### Required Extensions

Install these essential VS Code extensions:

1. **C# Dev Kit** (Microsoft) - Includes C# extension and debugging support
2. **C#** (Microsoft) - Language support
3. **.NET Install Tool** (Microsoft) - Optional, helps manage .NET versions

### Verify .NET Installation

Open VS Code terminal (`Ctrl+` ` or View → Terminal):

```bash
dotnet --version
dotnet --list-sdks
```

## Creating Projects

### Create a New Project

```bash
# Empty web application
dotnet new web -n MyWebApp

# Web API
dotnet new webapi -n MyApi

# MVC application
dotnet new mvc -n MyMvcApp

# Console application
dotnet new console -n MyConsoleApp

# Class library
dotnet new classlib -n MyLibrary

# Blazor Server
dotnet new blazorserver -n MyBlazorApp

# Blazor WebAssembly
dotnet new blazorwasm -n MyBlazorWasm
```

### List Available Templates

```bash
dotnet new list
```

### Open Project in VS Code

```bash
cd MyWebApp
code .
```

Or from VS Code: `File → Open Folder`

## Working with Solutions

### Create and Manage Solutions

```bash
# Create solution
dotnet new sln -n MySolution

# Add project to solution
dotnet sln add MyWebApp/MyWebApp.csproj

# Add multiple projects
dotnet sln add **/*.csproj

# List projects in solution
dotnet sln list

# Remove project from solution
dotnet sln remove MyWebApp/MyWebApp.csproj
```

### Multi-Project Solution Structure

```bash
# Create solution structure
mkdir MySolution
cd MySolution
dotnet new sln

# Create projects
dotnet new webapi -n MyApi
dotnet new classlib -n MyApi.Core
dotnet new xunit -n MyApi.Tests

# Add all to solution
dotnet sln add MyApi/MyApi.csproj
dotnet sln add MyApi.Core/MyApi.Core.csproj
dotnet sln add MyApi.Tests/MyApi.Tests.csproj

# Add project references
cd MyApi
dotnet add reference ../MyApi.Core/MyApi.Core.csproj
cd ../MyApi.Tests
dotnet add reference ../MyApi/MyApi.csproj
```

## Building and Running

### Build Commands

```bash
# Build project
dotnet build

# Build in Release mode
dotnet build -c Release

# Build specific project
dotnet build MyApi/MyApi.csproj

# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore
```

### Run Commands

```bash
# Run project
dotnet run

# Run with specific configuration
dotnet run -c Release

# Run and watch for changes (hot reload)
dotnet watch run

# Run specific project
dotnet run --project MyApi/MyApi.csproj

# Run with arguments
dotnet run -- arg1 arg2
```

## Package Management

### Managing NuGet Packages

```bash
# Add package
dotnet add package Newtonsoft.Json

# Add specific version
dotnet add package Newtonsoft.Json --version 13.0.1

# Add package to specific project
dotnet add MyApi/MyApi.csproj package Serilog

# Remove package
dotnet remove package Newtonsoft.Json

# List installed packages
dotnet list package

# List outdated packages
dotnet list package --outdated

# Update packages
dotnet add package PackageName
```

### Common Packages

```bash
# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Logging
dotnet add package Serilog.AspNetCore

# Testing
dotnet add package xunit
dotnet add package Moq
dotnet add package FluentAssertions

# JSON
dotnet add package Newtonsoft.Json
```

## Testing

### Create and Run Tests

```bash
# Create test project
dotnet new xunit -n MyApp.Tests

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests and collect coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test
dotnet test --filter "FullyQualifiedName~MyTestMethod"

# Run tests in watch mode
dotnet watch test
```

## Debugging in VS Code

### Launch Configuration

VS Code will auto-generate `.vscode/launch.json`. Example configuration:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/bin/Debug/net8.0/MyApp.dll",
      "args": [],
      "cwd": "${workspaceFolder}",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  ]
}
```

### Debug Commands

- **F5** - Start debugging
- **Ctrl+F5** - Start without debugging
- **F9** - Toggle breakpoint
- **F10** - Step over
- **F11** - Step into
- **Shift+F11** - Step out
- **Shift+F5** - Stop debugging

## Publishing and Deployment

### Publish Commands

```bash
# Publish for current platform
dotnet publish -c Release

# Publish for specific runtime
dotnet publish -c Release -r win-x64
dotnet publish -c Release -r linux-x64
dotnet publish -c Release -r osx-x64

# Self-contained deployment
dotnet publish -c Release -r win-x64 --self-contained true

# Single file publish
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true

# Trimmed publish (smaller size)
dotnet publish -c Release -r win-x64 -p:PublishTrimmed=true

# Specify output directory
dotnet publish -c Release -o ./publish
```

## Entity Framework Core

### EF Core CLI Commands

```bash
# Install EF Core tools globally
dotnet tool install --global dotnet-ef

# Update EF Core tools
dotnet tool update --global dotnet-ef

# Add migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script

# Drop database
dotnet ef database drop

# List migrations
dotnet ef migrations list

# Scaffold from existing database
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer
```

## Project Management

### Add Project References

```bash
# Add reference to another project
dotnet add reference ../MyLibrary/MyLibrary.csproj

# Remove reference
dotnet remove reference ../MyLibrary/MyLibrary.csproj

# List references
dotnet list reference
```

### Add Files

```bash
# Add new class
dotnet new class -n MyClass

# Add new interface
dotnet new interface -n IMyInterface
```

## Useful VS Code Shortcuts

### General

- **Ctrl+`** - Toggle terminal
- **Ctrl+Shift+`** - New terminal
- **Ctrl+P** - Quick file open
- **Ctrl+Shift+P** - Command palette
- **Ctrl+,** - Settings

### Code Navigation

- **F12** - Go to definition
- **Alt+F12** - Peek definition
- **Shift+F12** - Find all references
- **Ctrl+T** - Go to symbol in workspace
- **Ctrl+Shift+O** - Go to symbol in file
- **Ctrl+G** - Go to line

### Editing

- **Ctrl+Space** - Trigger IntelliSense
- **Ctrl+.** - Quick fix
- **Alt+Shift+F** - Format document
- **Ctrl+K Ctrl+C** - Comment line
- **Ctrl+K Ctrl+U** - Uncomment line
- **Ctrl+D** - Select next occurrence
- **Alt+Click** - Multiple cursors

### Refactoring

- **F2** - Rename symbol
- **Ctrl+Shift+R** - Refactor (with C# extension)

## Project Settings Files

### launchSettings.json

Located in `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Common Workflows

### Start a New Web API Project

```bash
# Create solution and project
mkdir MyApi
cd MyApi
dotnet new sln
dotnet new webapi -n MyApi
dotnet sln add MyApi/MyApi.csproj

# Open in VS Code
code .

# Run with hot reload
dotnet watch run
```

### Add EF Core to Project

```bash
# Add packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Install EF tools
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

### Create Multi-Project Solution

```bash
# Create structure
mkdir MySolution
cd MySolution
dotnet new sln

# Create projects
dotnet new webapi -n MySolution.Api
dotnet new classlib -n MySolution.Core
dotnet new classlib -n MySolution.Infrastructure
dotnet new xunit -n MySolution.Tests

# Add to solution
dotnet sln add **/*.csproj

# Add references
cd MySolution.Api
dotnet add reference ../MySolution.Core/MySolution.Core.csproj
dotnet add reference ../MySolution.Infrastructure/MySolution.Infrastructure.csproj

cd ../MySolution.Infrastructure
dotnet add reference ../MySolution.Core/MySolution.Core.csproj

cd ../MySolution.Tests
dotnet add reference ../MySolution.Api/MySolution.Api.csproj
```

## Tips and Best Practices

1. **Use `dotnet watch run`** for automatic reloading during development
2. **Enable Hot Reload** - it's on by default in .NET 6+
3. **Use Solution files** for multi-project applications
4. **Commit `.vscode` folder** to share settings with team (optional)
5. **Add `.gitignore`** - use `dotnet new gitignore`
6. **Use EditorConfig** for consistent code formatting
7. **Install C# Dev Kit** extension for best experience
8. **Use integrated terminal** instead of external terminal
9. **Set up launch configurations** for easier debugging
10. **Use tasks** (`.vscode/tasks.json`) for custom build commands

## Troubleshooting

### Common Issues

```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build

# Check SDK version
dotnet --info

# Repair C# extension
# In VS Code: Ctrl+Shift+P → "Developer: Reload Window"
```

### VS Code Not Finding .NET

1. Restart VS Code after installing .NET SDK
2. Check PATH environment variable
3. Verify with `dotnet --version` in terminal
4. Reinstall C# Dev Kit extension

## Resources

- [.NET CLI Documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/)
- [VS Code C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

