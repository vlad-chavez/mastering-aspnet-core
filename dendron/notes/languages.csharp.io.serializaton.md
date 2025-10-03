---
id: 6yxvzb4x5ocn42ye08tfain
title: Serializaton
desc: ''
updated: 1759512390331
created: 1759510295762
---
Of course. Here is your text formatted in markdown.

# JSON and XML Serialization in C\#

**Serialization** is the process of converting an object into a format that can be stored or transmitted, and **deserialization** is converting it back to an object.

-----

## JSON Serialization in C\#

C\# offers multiple ways to work with JSON. The modern approach uses **`System.Text.Json`** (built-in, .NET Core 3.0+), while the legacy approach uses **`Newtonsoft.Json`** (Json.NET).

### Using System.Text.Json (Modern, Recommended)

#### Basic Serialization and Deserialization

```csharp
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// Define a class
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}

class Program
{
    static void Main()
    {
        // Create an object
        var user = new User
        {
            Id = 123,
            Name = "John Doe",
            Email = "john@example.com",
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        // SERIALIZE: Object → JSON string
        string jsonString = JsonSerializer.Serialize(user);
        Console.WriteLine(jsonString);
        // Output: {"Id":123,"Name":"John Doe","Email":"john@example.com","CreatedAt":"2025-10-03T12:17:55.123","IsActive":true}

        // DESERIALIZE: JSON string → Object
        User deserializedUser = JsonSerializer.Deserialize<User>(jsonString);
        Console.WriteLine($"Name: {deserializedUser.Name}");
        // Output: Name: John Doe
    }
}
```

#### Pretty Printing (Formatted JSON)

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true  // Makes JSON readable
};
string prettyJson = JsonSerializer.Serialize(user, options);
Console.WriteLine(prettyJson);
/* Output:
{
  "Id": 123,
  "Name": "John Doe",
  "Email": "john@example.com",
  "CreatedAt": "2025-10-03T12:17:55.123",
  "IsActive": true
}
*/
```

#### Customizing Property Names

```csharp
public class User
{
    [JsonPropertyName("user_id")]  // Custom JSON property name
    public int Id { get; set; }
    [JsonPropertyName("full_name")]
    public string Name { get; set; }
    public string Email { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}
// Serialized output will now use the custom names
// {"user_id":123,"full_name":"John Doe",...}
```

#### Using camelCase Naming Convention

```csharp
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
string json = JsonSerializer.Serialize(user, options);
// Output: {"id":123,"name":"John Doe","email":"john@example.com",...}
```

#### Ignoring Properties

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]  // This property won't be serialized
    public string Password { get; set; }
    
    public string Email { get; set; }
}

var user = new User
{
    Id = 123,
    Name = "John",
    Password = "secret123",  // Won't appear in JSON
    Email = "john@example.com"
};

string json = JsonSerializer.Serialize(user);
// Output: {"Id":123,"Name":"John","Email":"john@example.com"}
```

#### Handling Null Values

```csharp
// Option 1: Include null values (default)
var optionsInclude = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.Never
};

// Option 2: Ignore null values
var optionsIgnore = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

var userWithNull = new User { Id = 123, Name = "John", MiddleName = null };

string jsonWithNull = JsonSerializer.Serialize(userWithNull, optionsInclude);
// Output: {"Id":123,"Name":"John","MiddleName":null}

string jsonWithoutNull = JsonSerializer.Serialize(userWithNull, optionsIgnore);
// Output: {"Id":123,"Name":"John"}
```

#### Working with Collections

```csharp
var users = new List<User>
{
    new User { Id = 1, Name = "John" },
    new User { Id = 2, Name = "Jane" }
};

// Serialization
string jsonArray = JsonSerializer.Serialize(users);
// Output: [{"Id":1,"Name":"John",...},{"Id":2,"Name":"Jane",...}]

// Deserialization
List<User> userList = JsonSerializer.Deserialize<List<User>>(jsonArray);
Console.WriteLine($"Count: {userList.Count}");
// Output: Count: 2
```

### Using Newtonsoft.Json (Json.NET) - Legacy but Feature-Rich

```csharp
using Newtonsoft.Json;
using System;

// ... User class definition ...

class Program
{
    static void Main()
    {
        var user = new User { Id = 123, Name = "John", Email = "john@example.com" };

        // Serialize
        string json = JsonConvert.SerializeObject(user);
        Console.WriteLine(json);

        // Serialize with formatting
        string prettyJson = JsonConvert.SerializeObject(user, Formatting.Indented);
        Console.WriteLine(prettyJson);

        // Deserialize
        User deserializedUser = JsonConvert.DeserializeObject<User>(json);
        Console.WriteLine(deserializedUser.Name);
    }
}
```

-----

## XML Serialization in C\#

XML serialization uses the `System.Xml.Serialization` namespace and relies on attributes to control the output.

### Basic XML Serialization

```csharp
using System;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("User")]  // Root element name
public class User
{
    [XmlElement("UserId")]
    public int Id { get; set; }
    [XmlElement("FullName")]
    public string Name { get; set; }
    [XmlElement("EmailAddress")]
    public string Email { get; set; }
    [XmlAttribute("IsActive")]  // This becomes an attribute, not an element
    public bool IsActive { get; set; }
}

class Program
{
    static void Main()
    {
        var user = new User { Id = 123, Name = "John Doe", IsActive = true };

        // SERIALIZE: Object → XML
        var serializer = new XmlSerializer(typeof(User));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, user);
            string xml = writer.ToString();
            Console.WriteLine(xml);
        }

        // DESERIALIZE: XML → Object
        string xmlString = @"<User IsActive='true'><UserId>123</UserId>...</User>";
        using (var reader = new StringReader(xmlString))
        {
            var deserializedUser = (User)serializer.Deserialize(reader);
            Console.WriteLine($"Name: {deserializedUser.Name}");
        }
    }
}
```

**Serialized XML Output:**

```xml
<?xml version="1.0" encoding="utf-16"?>
<User IsActive="true">
  <UserId>123</UserId>
  <FullName>John Doe</FullName>
  <EmailAddress>john@example.com</EmailAddress>
</User>
```

### XML with Collections

```csharp
public class UserList
{
    [XmlArray("Users")]      // Wrapping element
    [XmlArrayItem("User")]   // Individual item element
    public List<User> Users { get; set; }
}

// ... User class definition ...
```

**Serialized XML Output:**

```xml
<?xml version="1.0" encoding="utf-16"?>
<UserList>
  <Users>
    <User>
      <Id>1</Id>
      <Name>John</Name>
    </User>
    <User>
      <Id>2</Id>
      <Name>Jane</Name>
    </User>
  </Users>
</UserList>
```

-----

## JSON vs XML Serialization Comparison

### Size Comparison

For the same data, JSON is typically more concise.
**JSON:**

```json
{
  "Id": 123,
  "Name": "John Doe",
  "IsActive": true
}
```

*Size: \~55 bytes*

**XML:**

```xml
<User IsActive="true">
  <Id>123</Id>
  <Name>John Doe</Name>
</User>
```

*Size: \~95 bytes (excluding XML declaration)*

### Performance Comparison

`System.Text.Json` is highly optimized and generally much faster than `XmlSerializer`. A benchmark test serializing and deserializing an object 100,000 times shows a significant difference.

```csharp
// Typical result for 100,000 iterations:
// JSON Time: ~30ms
// XML Time: ~250ms
```

### Error Handling

Both libraries throw specific exceptions for parsing errors.
**JSON Error Handling**

```csharp
string invalidJson = "{\"Id\":123,\"Name\":\"John\",}"; // Invalid trailing comma
try
{
    User user = JsonSerializer.Deserialize<User>(invalidJson);
}
catch (JsonException ex)
{
    Console.WriteLine($"JSON Error: {ex.Message}");
}
```

**XML Error Handling**

```csharp
string invalidXml = "<User><Id>123</Name></User>"; // Mismatched tags
try
{
    var serializer = new XmlSerializer(typeof(User));
    using (var sr = new StringReader(invalidXml))
    {
        var user = (User)serializer.Deserialize(sr);
    }
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"XML Error: {ex.InnerException?.Message}");
}
```

-----

## Best Practices

### JSON Best Practices

  * ✅ **Use `System.Text.Json`** for all new .NET projects. It's modern, fast, and built-in.
  * ✅ **Use `camelCase`** property naming for interoperability with JavaScript and other web technologies.
  * ✅ **Use async methods** (`SerializeAsync`/`DeserializeAsync`) for file and network I/O to avoid blocking threads.
  * ✅ **Handle exceptions** (`JsonException`) when deserializing untrusted input.

### XML Best Practices

  * ✅ **Reuse `XmlSerializer` instances** when possible, as they have a performance overhead on the first use.
  * ✅ **Use `using` statements** for `StreamReader`/`StreamWriter` to ensure resources are properly disposed.
  * ✅ **Be explicit with attributes** (`[XmlRoot]`, `[XmlElement]`, `[XmlAttribute]`) to create a clear and stable contract.
  * ✅ **Handle exceptions** (`InvalidOperationException`) during deserialization.

-----

## Summary

**JSON is the modern standard:**

  * Smaller size & faster performance.
  * The preferred format for web APIs, mobile apps, and microservices.
  * Natively supported by JavaScript, making it ideal for web development.
  * Use **`System.Text.Json`** for new C\# projects.

**XML is still relevant:**

  * Excellent for complex, structured documents where validation, namespaces, and metadata (attributes) are critical.
  * Heavily used in **legacy systems**, enterprise applications (especially SOAP), and government/financial sectors.
  * Its verbosity can make it more self-descriptive than JSON.

The choice depends on your specific requirements, performance needs, and the ecosystem your application interacts with.