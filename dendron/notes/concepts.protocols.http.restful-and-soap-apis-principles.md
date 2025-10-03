---
id: shg3e618yjhjf9bwczvni9k
title: RESTful and SOAP APIs Principles
desc: ''
updated: 1759485783409
created: 1759485743790
---

RESTful and SOAP APIs
Let me explain these two major API architectural styles, their principles, differences, and when to use each.

REST (Representational State Transfer)
What is REST?
REST is an architectural style (not a protocol) for designing networked applications. It was introduced by Roy Fielding in 2000 and relies on stateless, client-server communication, typically using HTTP.
Core Principles of REST
1. Client-Server Architecture

Clear separation between client (user interface) and server (data storage)
They can evolve independently
Client doesn't need to know about data storage
Server doesn't need to know about UI

2. Stateless

Each request must contain all information needed to understand and process it
Server doesn't store client context between requests
Session state is kept entirely on the client

Example:
// ❌ Stateful (not RESTful)
Request 1: POST /login → Server stores session
Request 2: GET /profile → Server uses stored session

// ✅ Stateless (RESTful)
Request: GET /profile
Headers: Authorization: Bearer token123
// Every request contains authentication
3. Cacheable

Responses must define themselves as cacheable or non-cacheable
Improves performance by reducing server load

HTTP/1.1 200 OK
Cache-Control: max-age=3600
ETag: "abc123"

// Client can reuse this response for 1 hour
4. Uniform Interface
This is the most important constraint, with four sub-principles:
a) Resource Identification

Everything is a resource (user, product, order)
Each resource has a unique URI

/users/123
/products/456
/orders/789
b) Manipulation Through Representations

Resources are manipulated using representations (JSON, XML)
Client receives representation of resource, not the resource itself

c) Self-Descriptive Messages

Each message includes enough information to describe how to process it

Content-Type: application/json
Accept: application/json
d) HATEOAS (Hypermedia As The Engine Of Application State)

Responses include links to related resources
Client can navigate API dynamically

json{
  "id": 123,
  "name": "John Doe",
  "links": {
    "self": "/users/123",
    "posts": "/users/123/posts",
    "friends": "/users/123/friends"
  }
}
5. Layered System

Client can't tell if connected directly to server or through intermediary
Allows load balancers, caches, proxies

6. Code on Demand (Optional)

Server can extend client functionality by sending executable code
Example: JavaScript sent to browser


RESTful API Design Principles
Use HTTP Methods Correctly
GET    /users          # List all users (Read collection)
GET    /users/123      # Get specific user (Read single)
POST   /users          # Create new user (Create)
PUT    /users/123      # Update entire user (Update/Replace)
PATCH  /users/123      # Partially update user (Update/Modify)
DELETE /users/123      # Delete user (Delete)
Resource Naming Conventions
✅ Good REST URLs:
GET    /users                    # Collection (plural nouns)
GET    /users/123                # Specific resource
GET    /users/123/posts          # Nested resource
GET    /posts?author=123         # Query parameters for filtering
GET    /posts?sort=date&limit=10 # Pagination and sorting
❌ Bad REST URLs:
GET    /getUser?id=123           # Avoid verbs in URL
POST   /user/delete/123          # Use DELETE method instead
GET    /users/123/delete         # Wrong method for action
GET    /api/v1/getUserPosts      # Too RPC-like
Use Proper Status Codes
200 OK              # Successful GET, PUT, PATCH
201 Created         # Successful POST
204 No Content      # Successful DELETE
400 Bad Request     # Invalid request data
401 Unauthorized    # Missing/invalid authentication
403 Forbidden       # Authenticated but not authorized
404 Not Found       # Resource doesn't exist
409 Conflict        # Duplicate resource
422 Unprocessable   # Validation error
500 Server Error    # Server-side error
Versioning
# URL versioning (most common)
https://api.example.com/v1/users
https://api.example.com/v2/users

# Header versioning
Accept: application/vnd.example.v1+json

# Query parameter versioning
https://api.example.com/users?version=1

Complete REST API Example
User Management API:
# List users with pagination
GET /api/v1/users?page=1&limit=20
Response: 200 OK
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

# Get single user
GET /api/v1/users/1
Response: 200 OK
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

# Create user
POST /api/v1/users
Content-Type: application/json
{
  "name": "Alice Smith",
  "email": "alice@example.com",
  "password": "secret123"
}
Response: 201 Created
Location: /api/v1/users/3
{
  "id": 3,
  "name": "Alice Smith",
  "email": "alice@example.com",
  "created_at": "2025-10-03T14:30:00Z"
}

# Update user (full replacement)
PUT /api/v1/users/3
Content-Type: application/json
{
  "name": "Alice Johnson",
  "email": "alice.johnson@example.com"
}
Response: 200 OK

# Partial update
PATCH /api/v1/users/3
Content-Type: application/json
{
  "email": "newemail@example.com"
}
Response: 200 OK

# Delete user
DELETE /api/v1/users/3
Response: 204 No Content

# Get user's posts (nested resource)
GET /api/v1/users/1/posts
Response: 200 OK
{
  "data": [
    {
      "id": 101,
      "title": "My First Post",
      "user_id": 1,
      "created_at": "2025-10-01T12:00:00Z"
    }
  ]
}

# Search/filter users
GET /api/v1/users?email=john@example.com&role=admin
Response: 200 OK

SOAP (Simple Object Access Protocol)
What is SOAP?
SOAP is a protocol (not just an architectural style) for exchanging structured information using XML. It was developed in the late 1990s and is commonly used in enterprise environments.
Core Characteristics of SOAP
1. Protocol-Based

Strict standards and specifications
Can use multiple protocols: HTTP, SMTP, TCP, UDP
Most commonly uses HTTP/HTTPS

2. XML-Only

All messages are XML
Heavily structured and verbose
Strongly typed

3. Built-in Error Handling

Standardized fault elements
Detailed error information

4. WS- Standards*

WS-Security (authentication, encryption)
WS-AtomicTransaction (transaction handling)
WS-ReliableMessaging (guaranteed delivery)
WS-Addressing (routing)


SOAP Message Structure
A SOAP message has three main parts:
xml<?xml version="1.0"?>
<soap:Envelope 
  xmlns:soap="http://www.w3.org/2003/05/soap-envelope"
  xmlns:example="http://example.com">
  
  <!-- Optional: Header for metadata, authentication -->
  <soap:Header>
    <example:Authentication>
      <example:Username>admin</example:Username>
      <example:Password>secret</example:Password>
    </example:Authentication>
  </soap:Header>
  
  <!-- Required: Body contains the actual message -->
  <soap:Body>
    <example:GetUserRequest>
      <example:UserId>123</example:UserId>
    </example:GetUserRequest>
  </soap:Body>
  
</soap:Envelope>

SOAP Request/Response Example
Request: Get User Information
POST /api/userservice HTTP/1.1
Host: example.com
Content-Type: text/xml; charset=utf-8
SOAPAction: "http://example.com/GetUser"
Content-Length: 500

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
Response: User Information
HTTP/1.1 200 OK
Content-Type: text/xml; charset=utf-8
Content-Length: 650

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
Error Response (SOAP Fault)
xml<?xml version="1.0"?>
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

WSDL (Web Services Description Language)
SOAP services are described using WSDL files - XML documents that define:

Available operations (methods)
Input/output message formats
Data types
Service endpoints

WSDL Example (simplified):
xml<?xml version="1.0"?>
<definitions 
  name="UserService"
  targetNamespace="http://example.com/user"
  xmlns="http://schemas.xmlsoap.org/wsdl/">
  
  <!-- Data types -->
  <types>
    <xsd:schema>
      <xsd:element name="GetUserRequest">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="UserId" type="xsd:int"/>
          </xsd:sequence>
        </xsd:complexType>
      </xsd:element>
      
      <xsd:element name="GetUserResponse">
        <xsd:complexType>
          <xsd:sequence>
            <xsd:element name="User" type="UserType"/>
          </xsd:sequence>
        </xsd:complexType>
      </xsd:element>
    </xsd:schema>
  </types>
  
  <!-- Messages -->
  <message name="GetUserRequestMsg">
    <part name="parameters" element="GetUserRequest"/>
  </message>
  
  <message name="GetUserResponseMsg">
    <part name="parameters" element="GetUserResponse"/>
  </message>
  
  <!-- Port type (operations) -->
  <portType name="UserPortType">
    <operation name="GetUser">
      <input message="GetUserRequestMsg"/>
      <output message="GetUserResponseMsg"/>
    </operation>
  </portType>
  
  <!-- Binding (protocol details) -->
  <binding name="UserBinding" type="UserPortType">
    <soap:binding transport="http://schemas.xmlsoap.org/soap/http"/>
    <operation name="GetUser">
      <soap:operation soapAction="http://example.com/GetUser"/>
    </operation>
  </binding>
  
  <!-- Service endpoint -->
  <service name="UserService">
    <port name="UserPort" binding="UserBinding">
      <soap:address location="http://example.com/api/userservice"/>
    </port>
  </service>
  
</definitions>

REST vs SOAP: Detailed Comparison
Architecture vs Protocol
AspectRESTSOAPTypeArchitectural styleProtocolRulesGuidelines, flexibleStrict specificationsStandardsNo strict standardW3C standard

Message Format
AspectRESTSOAPFormatJSON, XML, HTML, plain textXML onlySizeLightweightVerbose, largerReadabilityHuman-readable (especially JSON)Complex XML structure
Example - Same data:
REST (JSON):
json{"id": 123, "name": "John", "email": "john@example.com"}
SOAP (XML):
xml<soap:Envelope xmlns:soap="...">
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

Transport Protocol
AspectRESTSOAPProtocolHTTP/HTTPS onlyHTTP, SMTP, TCP, UDP, JMSMethodsUses HTTP methods (GET, POST, etc.)Typically only POST

Security
AspectRESTSOAPSecurityHTTPS, OAuth, JWT tokensWS-Security (built-in)EncryptionTLS/SSL at transport layerMessage-level securityStandardsNo built-in standardWS-Security, WS-Trust
REST Security Example:
GET /api/users/123
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
SOAP Security Example:
xml<soap:Header>
  <wsse:Security>
    <wsse:UsernameToken>
      <wsse:Username>admin</wsse:Username>
      <wsse:Password Type="PasswordDigest">...</wsse:Password>
    </wsse:UsernameToken>
  </wsse:Security>
</soap:Header>

Error Handling
AspectRESTSOAPErrorsHTTP status codesSOAP Fault elementStructureFlexibleStandardizedDetailVaries by implementationComprehensive
REST Error:
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "error": "User not found",
  "code": "USER_NOT_FOUND",
  "details": "No user exists with ID 123"
}
SOAP Fault:
xml<soap:Fault>
  <faultcode>soap:Client</faultcode>
  <faultstring>User not found</faultstring>
  <detail>
    <error>No user exists with ID 123</error>
  </detail>
</soap:Fault>

State Management
AspectRESTSOAPStateStatelessCan be stateful or statelessSessionNo server-side sessionCan maintain session

Caching
AspectRESTSOAPCachingBuilt-in HTTP cachingNo built-in cachingPerformanceBetter for read-heavy operationsGenerally slower

Discoverability
AspectRESTSOAPAPI DescriptionOptional (OpenAPI/Swagger)WSDL (required)Auto-generationTools availableClient code auto-generated from WSDL

Transaction Support
AspectRESTSOAPACID TransactionsNot built-inWS-AtomicTransactionReliabilityManual implementationWS-ReliableMessaging

Bandwidth Usage
AspectRESTSOAPData sizeSmaller (especially JSON)Larger (XML overhead)EfficiencyMore efficientLess efficient
Example bandwidth comparison:

REST/JSON: ~80 bytes
SOAP/XML: ~350 bytes (for same data)


When to Use REST
✅ Use REST when:

Public APIs - Social media, payment gateways, cloud services
Mobile applications - Bandwidth and battery constraints
Web applications - Most modern web apps
Microservices - Lightweight communication
Limited bandwidth - Mobile networks, IoT devices
Rapid development - Faster to implement
CRUD operations - Simple data operations
Stateless operations - No complex transactions

Examples:

Twitter API
GitHub API
Stripe Payment API
Google Maps API


When to Use SOAP
✅ Use SOAP when:

Enterprise applications - Banks, healthcare, government
High security requirements - WS-Security features
ACID transactions - Need guaranteed delivery
Formal contracts - WSDL defines strict contract
Async processing - Can use protocols beyond HTTP
Legacy system integration - Existing SOAP infrastructure
Stateful operations - Need to maintain conversation
Reliable messaging - Cannot afford message loss

Examples:

PayPal Payment Services (also offers REST)
Financial services (money transfers)
Healthcare systems (patient records)
Telecom services


Modern Alternatives
GraphQL

Query language for APIs
Client specifies exactly what data it needs
Single endpoint
Reduces over-fetching and under-fetching

graphqlquery {
  user(id: 123) {
    name
    email
    posts {
      title
      createdAt
    }
  }
}
gRPC

Google's RPC framework
Uses Protocol Buffers (binary format)
HTTP/2 based
High performance


Summary Table
FeatureRESTSOAPComplexitySimpleComplexLearning CurveEasySteepPerformanceFasterSlowerFlexibilityHighLowStandardsLooseStrictBest ForPublic APIs, web/mobileEnterprise, high-securityPopularityVery highDecliningFutureDominantNiche/legacy
Bottom line: REST has become the de facto standard for modern web APIs due to its simplicity and performance, while SOAP remains relevant in enterprise environments requiring strict security and transaction guarantees.