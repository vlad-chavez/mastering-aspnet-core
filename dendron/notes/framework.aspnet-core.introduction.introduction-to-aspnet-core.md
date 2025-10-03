---
id: mzspqifj1q206v8iregm0ck
title: Introduction to Aspnet Core
desc: ''
updated: 1759477634951
created: 1759431302785
---

# ASP.NET Core Fundamentals

**ASP.NET Core** is a complete rewrite of the original ASP.NET framework. It is a **cross-platform, high-performance, and open-source** framework from Microsoft for building modern, internet-connected, and cloud-ready applications and services.

***

## 🚀 Key Framework Pillars

| Pillar | Description |
| :--- | :--- |
| **Cross-Platform** | Applications can be developed and run on **Windows, Linux, and macOS**. This is achieved through the **.NET** platform (formerly .NET Core). |
| **High-Performance** | Built for speed and efficiency, making it one of the **fastest** web application frameworks available today (benchmarked against Node.js, Go, etc.). |
| **Open-Source** | Developed and maintained openly by Microsoft and a large community on **GitHub** (with over 1000+ contributors to the main repository). |
| **Cloud-Enabled** | Designed to work seamlessly with modern **cloud environments** and **containerization** technologies like Docker and Kubernetes, with native support for **Microsoft Azure**. |

***

## 🌐 Application Models (Project Types)

ASP.NET Core offers multiple distinct architectural patterns to suit different application needs:

| Model | Primary Use Case | Description |
| :--- | :--- | :--- |
| **ASP.NET Core MVC** | **Complex Web Applications** | Uses the Model-View-Controller pattern. Ideal for applications requiring clear separation of concerns, complex routing, and large-scale, testable projects. |
| **ASP.NET Core Web API** | **RESTful Services/Backends** | For creating APIs that expose data over HTTP to clients (mobile apps, SPAs, desktop apps). Focuses purely on data and business logic, without UI views. |
| **ASP.NET Core Razor Pages** | **Page-Focused Web Apps** | A simpler, page-centric model where the C# code (code-behind) and UI logic are kept together. Excellent for small-to-medium web applications and forms-over-data scenarios. |
| **ASP.NET Core Blazor** | **Full-Stack Web UI with C#** | Allows developers to build interactive client-side web UIs using C# instead of JavaScript. **Blazor WebAssembly** runs C# directly in the browser, while **Blazor Server** handles UI updates via SignalR connections. |

***

## ⚙️ Hosting and Servers

ASP.NET Core uses a modular, two-tier server architecture for high flexibility and performance:

* **Application Server (Web Host):**
    * **Kestrel:** This is the default, high-performance, cross-platform **HTTP server** built into ASP.NET Core. It handles direct HTTP requests.
* **Reverse Proxy Servers:**
    * For production, Kestrel is typically run behind a professional reverse proxy server for security, load balancing, and advanced request handling.
    * Supported proxies include **IIS** (on Windows), **Nginx** (on Linux), and **Apache**.
* **Containerization:**
    * Native support for packaging apps into **Docker** containers, which are often orchestrated by systems like **Kubernetes** in cloud environments.

