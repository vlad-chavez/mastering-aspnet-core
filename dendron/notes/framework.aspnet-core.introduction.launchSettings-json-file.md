---
id: cm495np8rkxtd0kqb7czuzh
title: launchSettings-json-file
desc: ''
updated: 1759473427939
created: 1759471444617
---

# launchSettings.json in ASP.NET Core

`launchSettings.json` is a configuration file used only during local development in ASP.NET Core projects. It controls how your application launches when you run it from Visual Studio, Visual Studio Code, or the `dotnet run` command.

## Location
The file is located at: `Properties/launchSettings.json`

## Key Purpose
It defines launch profiles - different ways to run your application during development. Each profile specifies:

- Which web server to use (IIS Express or Kestrel)
- Environment variables
- Application URLs
- Browser launch behavior

## Structure Example
```json
{
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "MyApp": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Important Properties
- `commandName`: Specifies the launch target (`Project` for Kestrel, `IISExpress` for IIS Express)
- `applicationUrl`: The URLs where your app will listen (only for Kestrel)
- `environmentVariables`: Sets environment variables like `ASPNETCORE_ENVIRONMENT`
- `launchBrowser`: Whether to automatically open a browser when the app starts

## Critical Points
- **Development only**: This file is ignored in production deployments
- **Not committed sensitive data**: Avoid storing secrets here since it's usually checked into source control
- **Profile selection**: You choose which profile to use when launching (dropdown in Visual Studio or `--launch-profile` flag with `dotnet run`)

This file makes it easy to switch between different development configurations without changing your actual application code or production settings.

## Switching Between Development Configurations
There are several ways to switch between the different profiles defined in `launchSettings.json`:

### 1. Visual Studio
**Using the dropdown:**
- Look at the top toolbar near the "Start" button
- Click the dropdown next to the green play button
- Select the profile you want (e.g., "IIS Express", "MyApp", "Docker", etc.)
- Click the play button to run with that profile

### 2. Visual Studio Code
**Using `launch.json`:**
- Press `F5` or go to Run and Debug panel
- Select the configuration from the dropdown at the top
- Click the play button

**Using the command palette:**
- Press `Ctrl+Shift+P` (or `Cmd+Shift+P` on Mac)
- Type "Select and Start Debugging"
- Choose your profile

### 3. Command Line (dotnet CLI)
**Specify a profile:**
```bash
dotnet run --launch-profile "MyApp"
```
**Without specifying (uses first profile):**
```bash
dotnet run
```

### 4. Environment-Specific Profiles
You can create multiple profiles for different scenarios:
```json
{
  "profiles": {
    "Development": {
      "commandName": "Project",
      "applicationUrl": "https://localhost:5001",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Staging": {
      "commandName": "Project",
      "applicationUrl": "https://localhost:5002",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Staging"
      }
    },
    "Development-API-Only": {
      "commandName": "Project",
      "launchBrowser": false,
      "applicationUrl": "https://localhost:5003",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ENABLE_SWAGGER": "false"
      }
    }
  }
}
```

Then switch between them using any of the methods above. This is useful for testing different configurations, ports, environment variables, or enabling/disabling features during development.
