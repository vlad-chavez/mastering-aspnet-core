---
id: shg3e618yjhjf9bwczvni9k
title: RESTful and SOAP APIs Principles
desc: ''
updated: 1759511547050
created: 1759485743790
---
# RESTful and SOAP APIs

Let me explain these two major API architectural styles, their principles, differences, and when to use each.

-----

## REST (Representational State Transfer)

### What is REST?

REST is an architectural style (not a protocol) for designing networked applications. It was introduced by Roy Fielding in 2000 and relies on stateless, client-server communication, typically using HTTP.

### Core Principles of REST

#### 1\. Client-Server Architecture

  * Clear separation between client (user interface) and server (data storage).
  * They can evolve independently.
  * The client doesn't need to know about data storage.
  * The server doesn't need to know about UI.

#### 2\. Stateless

  * Each request must contain all information needed to understand and process it.
  * The server doesn't store client context between requests.
  * Session state is kept entirely on the client.

**Example:**

```
// ❌ Stateful (not RESTful)
Request 1: POST /login → Server stores session
Request 2: GET /profile → Server uses stored session

// ✅ Stateless (RESTful)
Request: GET /profile
Headers: Authorization: Bearer token123
// Every request contains authentication
```

#### 3\. Cacheable

  * Responses must define themselves as cacheable or non-cacheable.
  * Improves performance by reducing server load.

<!-- end list -->

```http
HTTP/1.1 200 OK
Cache-Control: max-age=3600
ETag: "abc123"

// Client can reuse this response for 1 hour
```

#### 4\. Uniform Interface

This is the most important constraint, with four sub-principles:

**a) Resource Identification**

  * Everything is a resource (user, product, order).
  * Each resource has a unique URI.

<!-- end list -->

```
/users/123
/products/456
/orders/789
```

**b) Manipulation Through Representations**

  * Resources are manipulated using representations (JSON, XML).
  * The client receives a representation of the resource, not the resource itself.

**c) Self-Descriptive Messages**

  * Each message includes enough information to describe how to process it.

<!-- end list -->

```
Content-Type: application/json
Accept: application/json
```

**d) HATEOAS (Hypermedia As The Engine Of Application State)**

  * Responses include links to related resources.
  * The client can navigate the API dynamically.

<!-- end list -->

```json
{
  "id": 123,
  "name": "John Doe",
  "links": {
    "self": "/users/123",
    "posts": "/users/123/posts",
    "friends": "/users/123/friends"
  }
}
```

#### 5\. Layered System

  * The client can't tell if it's connected directly to the server or through an intermediary.
  * Allows for load balancers, caches, and proxies.

#### 6\. Code on Demand (Optional)

  * The server can extend client functionality by sending executable code.
  * Example: JavaScript sent to the browser.

### RESTful API Design Principles

#### Use HTTP Methods Correctly

```
GET    /users         # List all users (Read collection)
GET    /users/123     # Get specific user (Read single)
POST   /users         # Create new user (Create)
PUT    /users/123     # Update entire user (Update/Replace)
PATCH  /users/123     # Partially update user (Update/Modify)
DELETE /users/123     # Delete user (Delete)
```

#### Resource Naming Conventions

✅ **Good REST URLs:**

```
GET    /users                   # Collection (plural nouns)
GET    /users/123               # Specific resource
GET    /users/123/posts         # Nested resource
GET    /posts?author=123        # Query parameters for filtering
GET    /posts?sort=date&limit=10# Pagination and sorting
```

❌ **Bad REST URLs:**

```
GET    /getUser?id=123           # Avoid verbs in URL
POST   /user/delete/123          # Use DELETE method instead
GET    /users/123/delete         # Wrong method for action
GET    /api/v1/getUserPosts      # Too RPC-like
```

#### Use Proper Status Codes

  * **200 OK**: Successful GET, PUT, PATCH
  * **201 Created**: Successful POST
  * **204 No Content**: Successful DELETE
  * **400 Bad Request**: Invalid request data
  * **401 Unauthorized**: Missing/invalid authentication
  * **403 Forbidden**: Authenticated but not authorized
  * **404 Not Found**: Resource doesn't exist
  * **409 Conflict**: Duplicate resource
  * **422 Unprocessable**: Validation error
  * **500 Server Error**: Server-side error

#### Versioning

**URL versioning (most common)**
`https://api.example.com/v1/users`
`https://api.example.com/v2/users`

**Header versioning**
`Accept: application/vnd.example.v1+json`

**Query parameter versioning**
`https://api.example.com/users?version=1`

### Complete REST API Example

A User Management API:

**List users with pagination**
`GET /api/v1/users?page=1&limit=20`
**Response: 200 OK**

```json
{
  "data": [
    {"id": 1, "name": "John", "email": "john@example.com"},
    {"id": 2, "name": "Jane", "email": "jane@example.com"}
  ],
  "pagination": {
    "page": 1,
    "limit": 20,
    "total": 150,
    "links": {
      "next": "/api/v1/users?page=2&limit=20",
      "prev": null
    }
  }
}
```

**Get single user**
`GET /api/v1/users/1`
**Response: 200 OK**

```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "created_at": "2025-01-15T10:00:00Z",
  "links": {
    "self": "/api/v1/users/1",
    "posts": "/api/v1/users/1/posts",
    "comments": "/api/v1/users/1/comments"
  }
}
```

**Create user**
`POST /api/v1/users`

```json
{
  "name": "Alice Smith",
  "email": "alice@example.com",
  "password": "secret123"
}
```

**Response: 201 Created**
`Location: /api/v1/users/3`

```json
{
  "id": 3,
  "name": "Alice Smith",
  "email": "alice@example.com",
  "created_at": "2025-10-03T14:30:00Z"
}
```

**Update user (full replacement)**
`PUT /api/v1/users/3`

```json
{
  "name": "Alice Johnson",
  "email": "alice.johnson@example.com"
}
```

**Response: 200 OK**

**Partial update**
`PATCH /api/v1/users/3`

```json
{
  "email": "newemail@example.com"
}
```

**Response: 200 OK**

**Delete user**
`DELETE /api/v1/users/3`
**Response: 204 No Content**

-----

## SOAP (Simple Object Access Protocol)

### What is SOAP?

SOAP is a protocol (not just an architectural style) for exchanging structured information using XML. It was developed in the late 1990s and is commonly used in enterprise environments.

### Core Characteristics of SOAP

1.  **Protocol-Based**
      * Strict standards and specifications.
      * Can use multiple protocols: HTTP, SMTP, TCP, UDP.
      * Most commonly uses HTTP/HTTPS.
2.  **XML-Only**
      * All messages are XML.
      * Heavily structured and verbose.
      * Strongly typed.
3.  **Built-in Error Handling**
      * Standardized `fault` elements.
      * Detailed error information.
4.  **WS-\* Standards**
      * **WS-Security** (authentication, encryption)
      * **WS-AtomicTransaction** (transaction handling)
      * **WS-ReliableMessaging** (guaranteed delivery)
      * **WS-Addressing** (routing)

### SOAP Message Structure

A SOAP message has three main parts:

```xml
<?xml version="1.0"?>
<soap:Envelope 
  xmlns:soap="http://www.w3.org/2003/05/soap-envelope"
  xmlns:example="http://example.com">
  
  <soap:Header>
    <example:Authentication>
      <example:Username>admin</example:Username>
      <example:Password>secret</example:Password>
    </example:Authentication>
  </soap:Header>
  
  <soap:Body>
    <example:GetUserRequest>
      <example:UserId>123</example:UserId>
    </example:GetUserRequest>
  </soap:Body>
  
</soap:Envelope>
```

### SOAP Request/Response Example

**Request: Get User Information**

```http
POST /api/userservice HTTP/1.1
Host: example.com
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://example.com/GetUser"
Content-Length: 500
```

```xml
<?xml version="1.0"?>
<soap:Envelope 
  xmlns:soap="http://www.w3.org/2003/05/soap-envelope"
  xmlns:user="http://example.com/user">
  
  <soap:Header>
    <user:AuthToken>abc123xyz</user:AuthToken>
  </soap:Header>
  
  <soap:Body>
    <user:GetUserRequest>
      <user:UserId>123</user:UserId>
    </user:GetUserRequest>
  </soap:Body>
  
</soap:Envelope>
```

**Response: User Information**

```http
HTTP/1.1 200 OK
Content-Type: text/xml; charset=utf-8
Content-Length: 650
```

```xml
<?xml version="1.0"?>
<soap:Envelope 
  xmlns:soap="http://www.w3.org/2003/05/soap-envelope"
  xmlns:user="http://example.com/user">
  
  <soap:Body>
    <user:GetUserResponse>
      <user:User>
        <user:Id>123</user:Id>
        <user:Name>John Doe</user:Name>
        <user:Email>john@example.com</user:Email>
        <user:CreatedDate>2025-01-15T10:00:00Z</user:CreatedDate>
      </user:User>
    </user:GetUserResponse>
  </soap:Body>
  
</soap:Envelope>
```

### Error Response (SOAP Fault)

```xml
<?xml version="1.0"?>
<soap:Envelope 
  xmlns:soap="http://www.w3.org/2003/05/soap-envelope">
  
  <soap:Body>
    <soap:Fault>
      <soap:Code>
        <soap:Value>soap:Sender</soap:Value>
      </soap:Code>
      <soap:Reason>
        <soap:Text xml:lang="en">User not found</soap:Text>
      </soap:Reason>
      <soap:Detail>
        <error:UserError xmlns:error="http://example.com/error">
          <error:ErrorCode>404</error:ErrorCode>
          <error:Message>User with ID 123 does not exist</error:Message>
        </error:UserError>
      </soap:Detail>
    </soap:Fault>
  </soap:Body>
  
</soap:Envelope>
```

### WSDL (Web Services Description Language)

SOAP services are described using WSDL files—XML documents that define:

  * Available operations (methods)
  * Input/output message formats
  * Data types
  * Service endpoints

-----

## REST vs SOAP: Detailed Comparison

### Architecture vs Protocol

| Aspect    | REST                  | SOAP                   |
| :-------- | :-------------------- | :--------------------- |
| **Type** | Architectural style   | Protocol               |
| **Rules** | Guidelines, flexible  | Strict specifications  |
| **Standards** | No strict standard    | W3C standard           |

### Message Format

| Aspect         | REST                           | SOAP                        |
| :------------- | :----------------------------- | :-------------------------- |
| **Format** | JSON, XML, HTML, plain text    | XML only                    |
| **Size** | Lightweight                    | Verbose, larger             |
| **Readability** | Human-readable (especially JSON) | Complex XML structure       |

**Example - Same data:**
**REST (JSON):**

```json
{"id": 123, "name": "John", "email": "john@example.com"}
```

**SOAP (XML):**

```xml
<soap:Envelope xmlns:soap="...">
  <soap:Body>
    <GetUserResponse>
      <User>
        <Id>123</Id>
        <Name>John</Name>
        <Email>john@example.com</Email>
      </User>
    </GetUserResponse>
  </soap:Body>
</soap:Envelope>
```

### Transport Protocol

| Aspect    | REST                             | SOAP                        |
| :-------- | :------------------------------- | :-------------------------- |
| **Protocol** | HTTP/HTTPS only                  | HTTP, SMTP, TCP, UDP, JMS   |
| **Methods** | Uses HTTP methods (GET, POST, etc.) | Typically only POST         |

### Security

| Aspect      | REST                           | SOAP                          |
| :---------- | :----------------------------- | :---------------------------- |
| **Security** | HTTPS, OAuth, JWT tokens       | WS-Security (built-in)        |
| **Encryption** | TLS/SSL at transport layer     | Message-level security        |
| **Standards** | No built-in standard           | WS-Security, WS-Trust         |

### Error Handling

| Aspect      | REST                | SOAP                        |
| :---------- | :------------------ | :-------------------------- |
| **Errors** | HTTP status codes   | SOAP Fault element          |
| **Structure** | Flexible            | Standardized                |
| **Detail** | Varies by implementation | Comprehensive               |

### State Management

| Aspect    | REST          | SOAP                       |
| :-------- | :------------ | :------------------------- |
| **State** | Stateless     | Can be stateful or stateless |
| **Session** | No server-side session | Can maintain session         |

-----

## When to Use REST

✅ Use **REST** when:

  * **Public APIs** - Social media, payment gateways, cloud services
  * **Mobile applications** - Bandwidth and battery constraints
  * **Web applications** - Most modern web apps
  * **Microservices** - Lightweight communication
  * **Limited bandwidth** - Mobile networks, IoT devices
  * **Rapid development** - Faster to implement
  * **CRUD operations** - Simple data operations
  * **Stateless operations** - No complex transactions

**Examples:** Twitter API, GitHub API, Stripe Payment API, Google Maps API

## When to Use SOAP

✅ Use **SOAP** when:

  * **Enterprise applications** - Banks, healthcare, government
  * **High security requirements** - WS-Security features
  * **ACID transactions** - Need guaranteed delivery
  * **Formal contracts** - WSDL defines a strict contract
  * **Async processing** - Can use protocols beyond HTTP
  * **Legacy system integration** - Existing SOAP infrastructure
  * **Stateful operations** - Need to maintain a conversation
  * **Reliable messaging** - Cannot afford message loss

**Examples:** PayPal Payment Services (also offers REST), Financial services, Healthcare systems

-----

## Modern Alternatives

### GraphQL

A query language for APIs where the client specifies exactly what data it needs from a single endpoint. This reduces over-fetching and under-fetching.

```graphql
query {
  user(id: 123) {
    name
    email
    posts {
      title
      createdAt
    }
  }
}
```

### gRPC

Google's high-performance RPC framework that uses Protocol Buffers (a binary format) and is based on HTTP/2.

-----

## Summary Table

| Feature          | REST                               | SOAP                                     |
| :--------------- | :--------------------------------- | :--------------------------------------- |
| **Complexity** | Simple                             | Complex                                  |
| **Learning Curve** | Easy                               | Steep                                    |
| **Performance** | Faster                             | Slower                                   |
| **Flexibility** | High                               | Low                                      |
| **Standards** | Loose                              | Strict                                   |
| **Best For** | Public APIs, web/mobile            | Enterprise, high-security                |
| **Popularity** | Very high                          | Declining                                |
| **Future** | Dominant                           | Niche/legacy                             |

**Bottom line:** REST has become the de facto standard for modern web APIs due to its simplicity and performance, while SOAP remains relevant in enterprise environments requiring strict security and transaction guarantees.