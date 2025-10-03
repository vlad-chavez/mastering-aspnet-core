---
id: erq2xzhbfnwcjp1us15p1wy
title: Status Code and Headers
desc: ''
updated: 1759482076397
created: 1759479629063
---

 ## 🚦 HTTP Status Codes
**HTTP status codes** are three-digit numbers the server sends back to the client, indicating the result of the request. They're grouped into five categories.

---

### 1xx - Informational
> The request was received and is being processed. These are rarely seen by the average user.

* **`100 Continue`**: The server received the request headers, and the client should proceed to send the request body.
* **`101 Switching Protocols`**: The server is switching to a different protocol as requested by the client (e.g., upgrading to a WebSocket).
* **`102 Processing`**: The server has received and is processing the request, but no response is available yet.

### 2xx - Success
> The request was successfully received, understood, and accepted. ✅

* **`200 OK`**: The standard response for a successful request. This is the most common one you'll see.
* **`201 Created`**: The request was successful, and a new resource was created as a result. This is a typical response to a `POST` request.
* **`202 Accepted`**: The request has been accepted for processing, but the processing is not complete (ideal for asynchronous operations).
* **`204 No Content`**: The server successfully processed the request but has no content to return. This is often used for `DELETE` requests.
* **`206 Partial Content`**: The server is delivering only part of the resource, which is used for things like video streaming or resumable downloads.

### 3xx - Redirection
> Further action is needed by the client to complete the request. ↪️

* **`301 Moved Permanently`**: The requested resource has been permanently moved to a new URL. Search engines and browsers will update their links.
* **`302 Found`**: The resource is temporarily at a different URL. The client should use the original URL for future requests.
* **`304 Not Modified`**: The client's cached version of the resource is still valid, so the server doesn't need to send it again, saving bandwidth.
* **`307 Temporary Redirect`**: Similar to `302`, but it requires the client to use the same HTTP method for the new request.
* **`308 Permanent Redirect`**: Similar to `301`, but it requires the client to use the same HTTP method for the new request.

### 4xx - Client Errors
> The request contains bad syntax or cannot be fulfilled because of an error on the client's side. 🛑

* **`400 Bad Request`**: The server cannot process the request due to a client error (e.g., malformed JSON, missing required fields).
* **`401 Unauthorized`**: Authentication is required, and the client either hasn't provided credentials or the credentials are invalid.
* **`403 Forbidden`**: The client is authenticated, but they do not have the necessary permissions to access the resource.
* **`404 Not Found`**: The server cannot find the requested resource. This is one of the most famous status codes on the web.
* **`405 Method Not Allowed`**: The HTTP method used (e.g., `GET`, `POST`) is not supported for this resource.
* **`409 Conflict`**: The request conflicts with the current state of the server (e.g., trying to create a user with an email that already exists).
* **`429 Too Many Requests`**: The user has sent too many requests in a given amount of time (**rate limiting**).

### 5xx - Server Errors
> The server failed to fulfill a valid request because of an error on the server's side. 💥

* **`500 Internal Server Error`**: A generic error message indicating that something went wrong on the server, but the server can't be more specific.
* **`502 Bad Gateway`**: The server was acting as a gateway or proxy and received an invalid response from an upstream server.
* **`503 Service Unavailable`**: The server is temporarily down or overloaded (e.g., for maintenance).
* **`504 Gateway Timeout`**: The server was acting as a gateway and did not receive a timely response from the upstream server.

---

## 🏷️ HTTP Headers
**HTTP headers** are key-value pairs that pass additional information and metadata between the client and the server with a request or response.

### General Headers
> Can be used in both requests and responses.

* **`Date`**: The date and time the message was sent.
* **`Connection`**: Controls whether the network connection stays open after the current transaction finishes (e.g., `keep-alive`).
* **`Cache-Control`**: Directives for **caching** mechanisms in both requests and responses (e.g., `no-cache`, `max-age=3600`).

### Request Headers
> Sent by the client to the server.

* **`Host`**: The domain name of the server (and optionally the port). **Required** in HTTP/1.1.
* **`User-Agent`**: A string that identifies the client's application, operating system, and version (e.g., your browser).
* **`Accept`**: Informs the server which content types the client can understand (e.g., `application/json`).
* **`Authorization`**: Contains the credentials to authenticate a user with the server (e.g., `Bearer <token>`).
* **`Cookie`**: Sends cookies from the client back to the server.
* **`Origin`**: Used in **CORS** (Cross-Origin Resource Sharing) requests to indicate where the request originated from.

### Response Headers
> Sent by the server to the client.

* **`Content-Type`**: The MIME type of the resource in the response body (e.g., `text/html`, `image/png`).
* **`Content-Length`**: The size of the response body in bytes.
* **`Set-Cookie`**: An instruction to the client to store a cookie.
* **`Location`**: Used in redirection (`3xx` status codes) or to specify the URL of a newly created resource (`201` status code).
* **`ETag`**: An identifier for a specific version of a resource, used for **caching**.
* **`Access-Control-Allow-Origin`**: A **CORS** header that specifies which origins are allowed to access the resource.
* **`WWW-Authenticate`**: Sent with a `401 Unauthorized` response to define the authentication method that should be used.

### Practical Examples

#### Example 1: API Request with Authentication
```http
POST /api/orders HTTP/1.1
Host: shop.example.com
Content-Type: application/json
Authorization: Bearer eyJhbGc...token

{
  "product_id": 789,
  "quantity": 2
}
```
**Response:**
```http
HTTP/1.1 201 Created
Content-Type: application/json
Location: /api/orders/1234
Set-Cookie: cart_id=xyz; Path=/; Secure; HttpOnly

{
  "order_id": 1234,
  "status": "pending",
  "total": 49.99
}
```

#### Example 2: Caching Scenario
**Initial Request:**
```http
GET /api/articles/123 HTTP/1.1
Host: blog.example.com
```
**Response with ETag:**
```http
HTTP/1.1 200 OK
Content-Type: application/json
ETag: "v1.0"
Cache-Control: max-age=3600

{
  "title": "HTTP Guide",
  "content": "..."
}
```
**Subsequent Request:**
```http
GET /api/articles/123 HTTP/1.1
Host: blog.example.com
If-None-Match: "v1.0"
```
**Response (if unchanged):**
```http
HTTP/1.1 304 Not Modified
ETag: "v1.0"
Cache-Control: max-age=3600
```
(No body is sent, saving bandwidth!)

### Custom Headers
You can create custom headers for your application. The convention is to prefix them with `X-`, though this is no longer required by the standard.
* `X-API-Version: 2.0`
* `X-Request-ID: 550e8400-e29b-41d4-a716-446655440000`
* `X-Rate-Limit-Remaining: 95`

Though the X- prefix is deprecated in the standard, it's still widely used.

Understanding status codes and headers is essential for debugging, building robust APIs, and optimizing web performance!