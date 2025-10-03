---
id: azgavn9ki1gnwmgx3x3dvpi
title: HTTP Methods
desc: ''
updated: 1759481566650
created: 1759478937866
---

## HTTP Methods (Verbs)
HTTP methods define the action you want to perform on a resource. Think of them as verbs in a sentence where the URL is the noun. Here are the main methods:

---

### Primary HTTP Methods

#### GET
* **Purpose**: Retrieve data from the server.
* **Characteristics**:
    * Read-only operation.
    * Data sent via URL query parameters.
    * Can be cached and bookmarked.
    * Should have no side effects (**idempotent** and **safe**).
* **Example**: Retrieves a list of electronics products.
    ```http
    GET /api/products?category=electronics&limit=10
    ```

#### POST
* **Purpose**: Submit data to create a new resource.
* **Characteristics**:
    * Data sent in the request body.
    * Creates new resources on the server.
    * **Not idempotent** (repeating creates multiple resources).
    * Cannot be cached.
* **Example**: Creates a new user account.
    ```http
    POST /api/users
    ```
    **Body:**
    ```json
    {"name": "Alice", "email": "alice@example.com"}
    ```

#### PUT
* **Purpose**: Update or replace an entire resource.
* **Characteristics**:
    * Data sent in the request body.
    * Replaces the entire resource.
    * **Idempotent** (repeating has the same effect).
    * If the resource doesn't exist, it may create it.
* **Example**: Replaces all data for user 123. If you omit a field, it might be removed.
    ```http
    PUT /api/users/123
    ```
    **Body:**
    ```json
    {"name": "Alice Smith", "email": "alice@example.com", "age": 30}
    ```

#### PATCH
* **Purpose**: Partially update a resource.
* **Characteristics**:
    * Data sent in the request body.
    * Updates only specified fields.
    * More efficient than `PUT` for small changes.
    * May or may not be idempotent.
* **Example**: Updates only the email field, leaving name and age unchanged.
    ```http
    PATCH /api/users/123
    ```
    **Body:**
    ```json
    {"email": "newemail@example.com"}
    ```

#### DELETE
* **Purpose**: Remove a resource.
* **Characteristics**:
    * Deletes the specified resource.
    * **Idempotent** (deleting multiple times has the same effect as deleting once).
    * May or may not have a request body.
* **Example**: Deletes the user with ID 123.
    ```http
    DELETE /api/users/123
    ```

---

### Less Common Methods

#### HEAD
Same as `GET` but retrieves only the headers, not the response body. Used for checking if a resource exists or getting metadata without downloading the full content.
```http
HEAD /api/large-file.pdf
```

#### OPTIONS
Describes the communication options for the target resource. Used for CORS (Cross-Origin Resource Sharing) preflight requests to check what methods are allowed.
```http
OPTIONS /api/users
```
**Response:** `Allow: GET, POST, PUT, DELETE`

#### CONNECT
Establishes a tunnel to the server. Used by proxies for HTTPS connections.

#### TRACE
Performs a message loop-back test. Used for debugging but is rarely used and often disabled for security reasons.

---

### Key Concepts

#### Idempotency
An operation is **idempotent** if performing it multiple times has the same effect as performing it once.
* **Idempotent**: `GET`, `PUT`, `DELETE`, `HEAD`, `OPTIONS`
* **Not idempotent**: `POST` (creates multiple resources)
* **Maybe idempotent**: `PATCH` (depends on implementation)

#### Safety
A method is **safe** if it doesn't modify resources on the server.
* **Safe**: `GET`, `HEAD`, `OPTIONS`
* **Unsafe**: `POST`, `PUT`, `PATCH`, `DELETE`

---

### REST API Convention
When building RESTful APIs, methods map to CRUD (Create, Read, Update, Delete) operations:

| HTTP Method | CRUD Operation | Example |
| :--- | :--- | :--- |
| **POST** | Create | Create a new article |
| **GET** | Read | Retrieve article(s) |
| **PUT/PATCH** | Update | Modify an article |
| **DELETE** | Delete | Remove an article |

**RESTful Example**:
```http
GET    /api/articles       - List all articles
GET    /api/articles/5     - Get article #5
POST   /api/articles       - Create a new article
PUT    /api/articles/5     - Update article #5 (full replacement)
PATCH  /api/articles/5     - Update article #5 (partial)
DELETE /api/articles/5     - Delete article #5
```

---

### PUT vs. PATCH Example
**Original Resource:**
```json
{
  "id": 123,
  "name": "John Doe",
  "email": "john@example.com",
  "age": 25
}
```

**Using `PUT` (replaces the entire resource):**
```http
PUT /api/users/123
Body: {"name": "John Smith"}

Result: {"id": 123, "name": "John Smith"}
// email and age are gone!
```
**Using PATCH (updates specific fields):**
```html
PATCH /api/users/123
Body: {"name": "John Smith"}

Result: {"id": 123, "name": "John Smith", "email": "john@example.com", "age": 25}
// email and age are preserved
```

The choice of HTTP method communicates your intent to both the server and other developers, making APIs more intuitive and predictable.