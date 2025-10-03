---
id: acgaru10detr741w0p1aopu
title: Questions
desc: ''
updated: 1759478123537
created: 1759430061592
---


## Section 1 - Introduction

### What is ASP.NET Core?

ASP.NET Core is a cross-platform, high-performance, open-source framework for building modern, cloud-based web applications and services using .NET.

### What are the features of ASP.NET Core?

- **Cross-Platform Support** — Runs on Windows, macOS, and Linux, unlike traditional ASP.NET which was tied to Windows and IIS.

- **High Performance** — Benchmarks show it handling millions of requests per second, outperforming alternatives like Node.js in certain scenarios.

- **Modular and Lightweight** — Developers can include only needed components, reducing overhead.

- **Security** — Built-in protections against XSS, CSRF, and support for authentication protocols, multi-factor auth, and external providers like Google.

- **Unified Framework** — Supports full-stack development, including frontend (Blazor) and backend, with tools for APIs, real-time apps (SignalR), and microservices.

- **Cloud-Ready** — Easy deployment to Azure and other clouds, with scalability for enterprise workloads.

- **Open-Source Community** — Hosted on GitHub with contributions from thousands, and active support on Stack Overflow and Microsoft Q&A.

### What are the advantages of ASP.NET Core over ASP.NET (.NET Framework)?

ASP.NET Core offers several significant advantages over the traditional ASP.NET (.NET Framework):

**Cross-Platform Support**  
ASP.NET Core runs on Windows, Linux, and macOS, while ASP.NET is Windows-only. This gives you flexibility in deployment and development environments.

**Performance**  
ASP.NET Core is dramatically faster — often 10x or more in benchmarks. It's built with performance as a core design principle, featuring a lean, modular architecture and optimized request pipeline.

**Modern Architecture**  
ASP.NET Core uses a unified programming model for web UI and web APIs, with built-in dependency injection throughout the framework. The middleware pipeline is more flexible and composable than the HTTP modules/handlers model in ASP.NET.

**Deployment Flexibility**  
You can deploy ASP.NET Core apps self-contained with the runtime included, or framework dependent. You can also run multiple versions side-by-side on the same server without conflicts.

**Open Source and Community-Driven**  
ASP.NET Core is fully open source on GitHub with active community contributions. Development happens transparently with public roadmaps.

**Cloud-Optimized**  
It's designed for containerization and microservices, with smaller memory footprint and faster startup times — ideal for Docker, Kubernetes, and cloud platforms.

**Active Development**  
ASP.NET Core receives regular updates with new features, while ASP.NET (.NET Framework) is in maintenance mode with only bug fixes and security updates.

**Modern Tooling**  
Better CLI tooling, improved project file format (SDK-style), and support for modern development workflows.

### What is ASP.NET Core meta package?

An ASP.NET Core metapackage is a NuGet package that simplifies dependency management by consolidating references to a collection of other NuGet packages. Instead of individually adding numerous packages required for a typical ASP.NET Core application, a single metapackage can be referenced, which in turn brings in all its dependent packages.

**Key characteristics and benefits:**

- **Consolidated Dependencies** — It groups related packages, such as those for MVC, Entity Framework Core, or other core functionalities, into a single, convenient package.

- **Simplified Management** — Reduces the burden of managing individual package versions and ensures compatibility between different components.

- **Shared Framework Reference** — Metapackages like `Microsoft.AspNetCore.App` refer to a shared framework, meaning the actual assemblies are installed on the machine and not bundled directly with the application, leading to smaller deployment sizes.

- **Faster Development** — Provides a quick way to set up a new ASP.NET Core project with a comprehensive set of necessary libraries.

### When do you choose classic ASP.NET MVC over ASP.NET Core?

The main reason to stick with ASP.NET would be if you have legacy applications with dependencies on technologies that haven't been ported to .NET Core (like WCF server-side, or certain third-party libraries).

### What is a web application framework, and what are its benefits?

A web application framework is a software platform that provides a structured foundation and reusable components for building web applications. It offers pre-written code, libraries, and tools that handle common web development tasks, allowing developers to focus on building application-specific features rather than reinventing the wheel.

#### Core Components of Web Frameworks

Web frameworks typically provide:

- **Routing** — Mapping URLs to specific code handlers
- **Request/Response handling** — Processing HTTP requests and generating responses
- **Templating engines** — Generating dynamic HTML content
- **Database integration** — Tools for data access and ORM (Object-Relational Mapping)
- **Authentication/Authorization** — User login and security features
- **Session management** — Tracking user state across requests
- **Form handling and validation** — Processing user input safely

#### Key Benefits

**Faster Development**  
Pre-built components and abstractions eliminate the need to write boilerplate code for common tasks like routing, database connections, and user authentication.

**Better Code Organization**  
Frameworks enforce architectural patterns (like MVC — Model-View-Controller) that promote clean separation of concerns and maintainable code structure.

**Security**  
Built-in protection against common vulnerabilities like SQL injection, cross-site scripting (XSS), and cross-site request forgery (CSRF).

**Standardization**  
Consistent coding practices across teams and projects, making it easier for developers to collaborate and maintain code.

**Scalability**  
Frameworks are designed with performance and scalability in mind, providing patterns and tools for building applications that can grow.

**Community and Ecosystem**  
Popular frameworks have large communities, extensive documentation, third-party libraries, and readily available solutions to common problems.

**Examples**  
ASP.NET Core, Django (Python), Ruby on Rails, Express.js (Node.js), Spring (Java), and Laravel (PHP) are all popular web application frameworks.

## Section - 2 

### What is Kestrel and what are advantages of Kestrel in Asp.Net Core?

### What is the difference between IIS and Kestrel? Why do we need two web servers?

### What is the purpose of launchSettings.json in asp.net core?

### What is generic host or HostBuilder in .NET Core?

### What is the purpose of the .csproj file?

### What is IIS?

### What does WebApplication.CreateBuilder() do?
