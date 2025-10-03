---
id: 5xwtqnarrmafzj6bpbfr9zy
title: Content Type Deep Dive
desc: ''
updated: 1759484975112
created: 1759481208769
---

# Content-Type Deep Dive

The **Content-Type** header tells the server what format the request body is in, so it knows how to parse it.  
Let me explain the most important content types, especially for form data.

---

## Form Data Content Types

### `application/x-www-form-urlencoded`

**What it is:**  
The default encoding for HTML forms. Data is encoded as key-value pairs, similar to URL query parameters.

**Encoding rules:**
- Spaces become `+` or `%20`
- Special characters are percent-encoded (`@` becomes `%40`)
- Key-value pairs separated by `&`
- Keys and values separated by `=`

**Example Request:**
```http
POST /api/login HTTP/1.1
Host: example.com
Content-Type: application/x-www-form-urlencoded
Content-Length: 45

username=john%40example.com&password=secret123&remember=true
```

**Decoded data:**
```
username: john@example.com
password: secret123
remember: true
```

**HTML Form Example:**
```html
<form method="POST" action="/login">
  <input name="username" value="john@example.com">
  <input name="password" type="password" value="secret123">
  <input name="remember" type="checkbox" checked>
  <button type="submit">Login</button>
</form>
```

**When to use:**
- Simple forms with text fields  
- No file uploads  
- Small amounts of data  

**Limitations:**
- Not efficient for large data  
- Cannot handle binary data (files)  
- Not human-readable in the request  

---

### `multipart/form-data`

**What it is:**  
Used for forms that include file uploads. Data is sent in multiple parts, each with its own headers.  
Each part is separated by a unique boundary string.

**Example Request:**
```http
POST /api/profile HTTP/1.1
Host: example.com
Content-Type: multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Length: 1024

------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="username"

johndoe
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="email"

john@example.com
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="avatar"; filename="profile.jpg"
Content-Type: image/jpeg

[binary image data here]
------WebKitFormBoundary7MA4YWxkTrZu0gW
Content-Disposition: form-data; name="resume"; filename="resume.pdf"
Content-Type: application/pdf

[binary PDF data here]
------WebKitFormBoundary7MA4YWxkTrZu0gW--
```

**Breakdown:**
- Each part starts with `-- + boundary string`
- `Content-Disposition` describes the field
- `filename` is included for file uploads
- Each file can have its own `Content-Type`
- Final boundary has `--` at the end too

**HTML Form Example:**
```html
<form method="POST" action="/profile" enctype="multipart/form-data">
  <input name="username" value="johndoe">
  <input name="email" value="john@example.com">
  <input name="avatar" type="file">
  <input name="resume" type="file">
  <button type="submit">Update Profile</button>
</form>
```

**When to use:**
- File uploads (images, documents, videos)  
- Mixing text fields with files  
- Large amounts of data  

**Advantages:**
- Can handle binary data  
- Can send multiple files  
- Each part has its own content type  

> **Note:** The boundary string is randomly generated and must not appear in any of the data.

---

### Comparison: URL-encoded vs Multipart

**Same form data in both formats:**

`application/x-www-form-urlencoded`:
```
name=John+Doe&age=30&bio=Software+developer+from+NYC
```

`multipart/form-data`:
```
------Boundary123
Content-Disposition: form-data; name="name"

John Doe
------Boundary123
Content-Disposition: form-data; name="age"

30
------Boundary123
Content-Disposition: form-data; name="bio"

Software developer from NYC
------Boundary123--
```

👉 Multipart is more verbose but necessary for files!

---

## JSON Content Types

### `application/json`

**What it is:**  
The most popular format for modern APIs. Data is sent as JSON (JavaScript Object Notation).

**Example Request:**
```http
POST /api/users HTTP/1.1
Host: example.com
Content-Type: application/json
Content-Length: 123

{
  "username": "johndoe",
  "email": "john@example.com",
  "age": 30,
  "preferences": {
    "theme": "dark",
    "notifications": true
  },
  "tags": ["developer", "javascript"]
}
```

**Advantages:**
- Human-readable  
- Supports nested objects and arrays  
- Native JavaScript support  
- Lightweight  

**When to use:**
- RESTful APIs  
- Modern web applications  
- Complex data structures  

**JavaScript Example:**
```javascript
fetch('/api/users', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    username: 'johndoe',
    email: 'john@example.com',
    age: 30
  })
});
```

---

### `application/ld+json` (JSON-LD)

**What it is:**  
JSON for Linked Data, used for structured data and SEO.

**Example:**
```json
{
  "@context": "https://schema.org",
  "@type": "Person",
  "name": "John Doe",
  "jobTitle": "Software Developer"
}
```

**When to use:** SEO, semantic web, structured data markup.

---

## XML Content Types

### `application/xml` or `text/xml`

**What it is:**  
Extensible Markup Language, older format still used in enterprise systems and SOAP APIs.

**Example Request:**
```http
POST /api/users HTTP/1.1
Host: example.com
Content-Type: application/xml
Content-Length: 256

<?xml version="1.0" encoding="UTF-8"?>
<user>
  <username>johndoe</username>
  <email>john@example.com</email>
  <age>30</age>
  <preferences>
    <theme>dark</theme>
    <notifications>true</notifications>
  </preferences>
</user>
```

**When to use:**
- Legacy systems  
- SOAP web services  
- Enterprise applications  
- Document-centric data  

---

## Text Content Types

### `text/plain`

**What it is:** Plain, unformatted text.

**Example:**
```http
POST /api/notes HTTP/1.1
Host: example.com
Content-Type: text/plain

This is just plain text content.
No formatting, no structure.
```

---

### `text/html`

**What it is:** HTML content.

**Example:**
```http
GET /page HTTP/1.1
Host: example.com

HTTP/1.1 200 OK
Content-Type: text/html; charset=utf-8

<!DOCTYPE html>
<html>
<head><title>Page</title></head>
<body><h1>Hello World</h1></body>
</html>
```

---

### `text/csv`

**What it is:** Comma-Separated Values, used for tabular data.

**Example:**
```http
POST /api/import HTTP/1.1
Host: example.com
Content-Type: text/csv

name,email,age
John Doe,john@example.com,30
Jane Smith,jane@example.com,25
```

---

## Binary Content Types

### `application/octet-stream`

**What it is:**  
Generic binary data. Used when you don't know or don't want to specify the exact file type.

**Example:**
```http
POST /api/upload HTTP/1.1
Host: example.com
Content-Type: application/octet-stream
Content-Disposition: attachment; filename="document.bin"

[binary data]
```

**When to use:** Downloading/uploading files when type is unknown or generic.

---

## Image Types

- `image/jpeg`  
- `image/png`  
- `image/gif`  
- `image/svg+xml`  
- `image/webp`  

---

## Audio/Video Types

- `audio/mpeg`  
- `audio/wav`  
- `video/mp4`  
- `video/webm`  

---

## Document Types

- `application/pdf`  
- `application/msword`  
- `application/vnd.openxmlformats-officedocument.wordprocessingml.document`  
- `application/vnd.ms-excel`  
- `application/zip`  

---

## Special Cases

### `application/graphql`

**What it is:** Used for GraphQL queries.

**Example:**
```graphql
POST /graphql HTTP/1.1
Host: api.example.com
Content-Type: application/graphql

query {
  user(id: "123") {
    name
    email
  }
}
```

---

### `application/x-protobuf`

**What it is:** Protocol Buffers, a binary serialization format by Google.  

**When to use:** High-performance APIs, gRPC services.

---

### `text/event-stream`

**What it is:** Server-Sent Events (SSE) for real-time updates.

**Example:**
```http
GET /events HTTP/1.1
Host: example.com
Accept: text/event-stream

HTTP/1.1 200 OK
Content-Type: text/event-stream
Cache-Control: no-cache

data: {"message": "New notification"}

data: {"message": "Another update"}
```

---

## Charset Parameter

Many content types can include a `charset` parameter:

```http
Content-Type: text/html; charset=utf-8
Content-Type: application/json; charset=utf-8
Content-Type: text/plain; charset=iso-8859-1
```

👉 UTF-8 is the most common and recommended charset for international support.

---

## Practical Decision Tree

**Sending data to server?**

→ Is it a file upload?  
**YES:** Use `multipart/form-data`  

→ Is it an HTML form (no files)?  
**YES:** Use `application/x-www-form-urlencoded` (default) or `application/json` for modern apps  

→ Is it structured data (API)?  
**YES:** Use `application/json` (most common) or `application/xml` (legacy)  

→ Is it plain text?  
**YES:** Use `text/plain`  

→ Is it binary data (unknown type)?  
**YES:** Use `application/octet-stream`  

---

## Common Mistakes

❌ **Wrong:** Using JSON without proper content type
```javascript
fetch('/api/users', {
  method: 'POST',
  body: JSON.stringify({name: 'John'})
  // Missing: headers: {'Content-Type': 'application/json'}
});
```

✅ **Correct:**
```javascript
fetch('/api/users', {
  method: 'POST',
  headers: {'Content-Type': 'application/json'},
  body: JSON.stringify({name: 'John'})
});
```

❌ **Wrong:** Using url-encoded for file uploads
```html
<form method="POST" action="/upload">
  <input type="file" name="photo">
</form>
```

✅ **Correct:**
```html
<form method="POST" action="/upload" enctype="multipart/form-data">
  <input type="file" name="photo">
</form>
```

---

**Understanding content types is crucial for proper client-server communication and avoiding parsing errors!**
