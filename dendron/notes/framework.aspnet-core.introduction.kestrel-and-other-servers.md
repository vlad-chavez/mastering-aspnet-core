---
id: 7asyfqibnh8wwr00pcgkioj
title: Kestrel and Other Servers
desc: ''
updated: 1759471141418
created: 1759469534245
---
# ASP.NET Core Web Application Hosting Architecture

Let me break down how these components work together in ASP.NET Core web applications.

## **Kestrel - The Application Server**

**Kestrel** is a cross-platform, lightweight web server built into ASP.NET Core. It's the default web server that comes with every ASP.NET Core application.

**Key characteristics:**
- **Fast and efficient** - Optimized for high performance, it can handle thousands of concurrent connections
- **Cross-platform** - Runs on Windows, Linux, and macOS
- **Built-in** - Every ASP.NET Core app runs Kestrel by default when you hit F5 in Visual Studio or run `dotnet run`
- **HTTP/HTTPS support** - Handles HTTP/1.1, HTTP/2, and can be configured for HTTPS

**What it does:**
- Receives HTTP requests
- Passes them to your ASP.NET Core application pipeline
- Returns HTTP responses
- Manages the connection between the network and your application code

## **Reverse Proxy Servers (IIS, Nginx, Apache)**

A **reverse proxy** sits in front of Kestrel and acts as an intermediary between the internet and your application server.

### **Why use a reverse proxy with Kestrel?**

While Kestrel can run standalone in development, Microsoft recommends using a reverse proxy in production for several important reasons:

1. **Security & Attack Surface** - Reverse proxies are battle-tested against various web attacks and DDoS attempts
2. **Load Balancing** - Distribute traffic across multiple Kestrel instances
3. **SSL/TLS Termination** - Handle HTTPS encryption/decryption, offloading this work from Kestrel
4. **Static File Serving** - Efficiently serve static files (images, CSS, JS) without hitting your application
5. **URL Rewriting & Routing** - Advanced routing rules and URL manipulation
6. **Compression** - Handle response compression
7. **Caching** - Cache responses to reduce load on your application
8. **Multiple Applications** - Host multiple apps on the same server using different ports/domains

### **The Three Main Options:**

**IIS (Internet Information Services)**
- **Windows-only** solution
- Deep integration with Windows Server
- Uses the ASP.NET Core Module to forward requests to Kestrel
- Familiar to Windows administrators
- Good for organizations already invested in the Windows ecosystem

**Nginx**
- **Cross-platform** (most popular on Linux)
- Extremely fast and lightweight
- Excellent for high-traffic scenarios
- Simple configuration files
- Very efficient at serving static content
- Popular choice for modern deployments

**Apache**
- **Cross-platform**
- Long-established, mature web server
- Extensive module ecosystem
- More complex configuration than Nginx
- Good for organizations already using Apache infrastructure

## **How It All Works Together**

Here's the typical request flow:

```
Internet Request
    ↓
[Reverse Proxy: IIS/Nginx/Apache]
    ↓ (forwards to)
[Kestrel Server] (listening on localhost:5000)
    ↓
[ASP.NET Core Application Pipeline]
    ↓
Your Application Code (Controllers, Razor Pages, etc.)
```

**Example scenario with Nginx:**

1. User requests `https://yourapp.com/api/products`
2. Nginx receives the request on port 443 (HTTPS)
3. Nginx handles SSL termination
4. Nginx forwards the request to Kestrel at `http://localhost:5000/api/products`
5. Kestrel processes the request through your ASP.NET Core app
6. Your controller returns data
7. Kestrel sends response back to Nginx
8. Nginx sends response to the user

## **When to Use Each Setup**

**Development:** 
- Just Kestrel alone is fine - simple and fast

**Production (Windows):**
- IIS + Kestrel - natural fit for Windows environments

**Production (Linux/Docker):**
- Nginx + Kestrel - most common choice, lightweight and fast
- Apache + Kestrel - if you're already using Apache

**Production (Azure/Cloud):**
- Often just Kestrel with cloud-provided load balancing and security layers

The beauty of ASP.NET Core is its flexibility - you can deploy the same application with different hosting configurations based on your infrastructure needs!


# Learning Resources for ASP.NET Core Hosting Architecture

Here are excellent resources to deepen your understanding of Kestrel and reverse proxy configurations:

## Official Microsoft Documentation
Start here - these are the most authoritative sources:

- **ASP.NET Core Web Server Implementations**  
  [https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/)  
  Covers Kestrel, HTTP.sys, and IIS integration

- **Kestrel Web Server**  
  [https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel)  
  Complete guide to Kestrel configuration and options

- **Host ASP.NET Core on Windows with IIS**  
  [https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/)  
  Detailed IIS + Kestrel setup and configuration

- **Host ASP.NET Core on Linux with Nginx**  
  [https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx)  
  Step-by-step guide for Linux deployment

- **Host ASP.NET Core on Linux with Apache**  
  [https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-apache)  
  Apache configuration for ASP.NET Core

## Video Tutorials
- **Microsoft's Official Channel**  
  Search for "ASP.NET Core deployment" on Microsoft's YouTube channel  
  Look for sessions from .NET Conf covering hosting and deployment

- **Pluralsight Courses** (subscription required):  
  - "Deploying ASP.NET Core Applications"  
  - "ASP.NET Core Fundamentals"

- **YouTube Channels**:  
  - *IAmTimCorey* - Practical ASP.NET Core deployment tutorials  
  - *Nick Chapsas* - Modern .NET practices including hosting  
  - *Raw Coding* - Deep dives into ASP.NET Core infrastructure

## Hands-On Learning
- **Tutorial Sites**:  
  - *Microsoft Learn* - [https://docs.microsoft.com/en-us/learn/](https://docs.microsoft.com/en-us/learn/)  
    Search for "ASP.NET Core" paths with free, interactive modules

- **Nginx Documentation**  
  [https://nginx.org/en/docs/](https://nginx.org/en/docs/)  
  Specifically look at the "Reverse Proxy" section

- **Docker & ASP.NET Core**  
  [https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)  
  Modern deployment often uses containers

## Books
- *"ASP.NET Core in Action"* by Andrew Lock - Excellent chapter on hosting and deployment  
- *"Pro ASP.NET Core"* by Adam Freeman - Covers the complete stack including hosting

## Blog Resources
- *Andrew Lock's Blog* [](https://andrewlock.net) - "ASP.NET Core in Depth" series  
- *Scott Hanselman's Blog* - Practical deployment advice  
- *Rick Strahl's Web Log* - IIS and Kestrel configurations

## Recommended Learning Path
1. Start with Microsoft's official Kestrel documentation to understand the basics  
2. Follow the deployment guide for your target platform (IIS, Nginx, or Apache)  
3. Try a hands-on tutorial - actually deploy a simple app  
4. Watch video walkthroughs to see the configuration in action  
5. Experiment with Docker for a modern containerized approach

The Microsoft documentation is well-written and regularly updated, so it’s your best starting point. Hands-on practice will cement your understanding!