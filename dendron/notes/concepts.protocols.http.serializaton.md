---
id: 6yxvzb4x5ocn42ye08tfain
title: Serializaton
desc: ''
updated: 1759510299602
created: 1759510295762
---
JSON and XML Serialization in C#
Serialization is the process of converting an object into a format that can be stored or transmitted, and deserialization is converting it back to an object.

JSON Serialization in C#
C# offers multiple ways to work with JSON. The modern approach uses System.Text.Json (built-in, .NET Core 3.0+), while the legacy approach uses Newtonsoft.Json (Json.NET).
Using System.Text.Json (Modern, Recommended)
Basic Serialization and Deserialization
csharpusing System;
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
        // Output: {"Id":123,"Name":"John Doe","Email":"john@example.com","CreatedAt":"2025-10-03T14:30:00","IsActive":true}

        // DESERIALIZE: JSON string → Object
        User deserializedUser = JsonSerializer.Deserialize<User>(jsonString);
        Console.WriteLine($"Name: {deserializedUser.Name}");
        // Output: Name: John Doe
    }
}

Pretty Printing (Formatted JSON)
csharpvar options = new JsonSerializerOptions
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
  "CreatedAt": "2025-10-03T14:30:00",
  "IsActive": true
}
*/

Customizing Property Names
csharppublic class User
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

// Serialized output:
// {"user_id":123,"full_name":"John Doe","email":"john@example.com","created_at":"2025-10-03T14:30:00","is_active":true}

Using camelCase Naming Convention
csharpvar options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

string json = JsonSerializer.Serialize(user, options);
// Output: {"id":123,"name":"John Doe","email":"john@example.com",...}

Ignoring Properties
csharppublic class User
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
// Password is not included

Handling Null Values
csharppublic class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? MiddleName { get; set; }  // Nullable
    public string Email { get; set; }
}

// Option 1: Include null values (default)
var options1 = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.Never
};

// Option 2: Ignore null values
var options2 = new JsonSerializerOptions
{
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

var user = new User
{
    Id = 123,
    Name = "John",
    MiddleName = null,
    Email = "john@example.com"
};

string json1 = JsonSerializer.Serialize(user, options1);
// Output: {"Id":123,"Name":"John","MiddleName":null,"Email":"john@example.com"}

string json2 = JsonSerializer.Serialize(user, options2);
// Output: {"Id":123,"Name":"John","Email":"john@example.com"}

Working with Collections
csharp// List serialization
var users = new List<User>
{
    new User { Id = 1, Name = "John", Email = "john@example.com" },
    new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
};

string jsonArray = JsonSerializer.Serialize(users);
Console.WriteLine(jsonArray);
// Output: [{"Id":1,"Name":"John",...},{"Id":2,"Name":"Jane",...}]

// Deserialization
List<User> userList = JsonSerializer.Deserialize<List<User>>(jsonArray);
Console.WriteLine($"Count: {userList.Count}");
// Output: Count: 2

Complex Nested Objects
csharppublic class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }  // Nested object
    public List<string> Tags { get; set; }  // Array
}

var user = new User
{
    Id = 123,
    Name = "John Doe",
    Email = "john@example.com",
    Address = new Address
    {
        Street = "123 Main St",
        City = "New York",
        ZipCode = "10001"
    },
    Tags = new List<string> { "developer", "csharp", "dotnet" }
};

var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(user, options);
Console.WriteLine(json);

/* Output:
{
  "Id": 123,
  "Name": "John Doe",
  "Email": "john@example.com",
  "Address": {
    "Street": "123 Main St",
    "City": "New York",
    "ZipCode": "10001"
  },
  "Tags": [
    "developer",
    "csharp",
    "dotnet"
  ]
}
*/

File I/O with JSON
csharpusing System.IO;
using System.Text.Json;

// Write to file
var user = new User { Id = 123, Name = "John", Email = "john@example.com" };

using (FileStream fs = File.Create("user.json"))
{
    JsonSerializer.Serialize(fs, user, new JsonSerializerOptions { WriteIndented = true });
}

// Read from file
using (FileStream fs = File.OpenRead("user.json"))
{
    User loadedUser = JsonSerializer.Deserialize<User>(fs);
    Console.WriteLine($"Loaded: {loadedUser.Name}");
}

// Async versions
await using (FileStream fs = File.Create("user.json"))
{
    await JsonSerializer.SerializeAsync(fs, user);
}

await using (FileStream fs = File.OpenRead("user.json"))
{
    User loadedUser = await JsonSerializer.DeserializeAsync<User>(fs);
}

Handling Dates
csharppublic class Event
{
    public string Name { get; set; }
    public DateTime EventDate { get; set; }
}

var evt = new Event
{
    Name = "Conference",
    EventDate = new DateTime(2025, 10, 15, 14, 30, 0)
};

string json = JsonSerializer.Serialize(evt);
// Default format: {"Name":"Conference","EventDate":"2025-10-15T14:30:00"}

// Custom date format (requires custom converter)
public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}

var options = new JsonSerializerOptions();
options.Converters.Add(new CustomDateTimeConverter());

string customJson = JsonSerializer.Serialize(evt, options);
// Output: {"Name":"Conference","EventDate":"2025-10-15"}

Using Newtonsoft.Json (Json.NET) - Legacy but Feature-Rich
csharpusing Newtonsoft.Json;
using System;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

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

XML Serialization in C#
XML serialization uses System.Xml.Serialization namespace.
Basic XML Serialization
csharpusing System;
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

    [XmlAttribute("IsActive")]  // This becomes an attribute, not element
    public bool IsActive { get; set; }
}

class Program
{
    static void Main()
    {
        var user = new User
        {
            Id = 123,
            Name = "John Doe",
            Email = "john@example.com",
            IsActive = true
        };

        // SERIALIZE: Object → XML
        XmlSerializer serializer = new XmlSerializer(typeof(User));
        
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, user);
            string xml = sw.ToString();
            Console.WriteLine(xml);
        }

        /* Output:
        <?xml version="1.0" encoding="utf-16"?>
        <User IsActive="true">
          <UserId>123</UserId>
          <FullName>John Doe</FullName>
          <EmailAddress>john@example.com</EmailAddress>
        </User>
        */

        // DESERIALIZE: XML → Object
        string xmlString = @"
        <User IsActive='true'>
          <UserId>123</UserId>
          <FullName>John Doe</FullName>
          <EmailAddress>john@example.com</EmailAddress>
        </User>";

        using (StringReader sr = new StringReader(xmlString))
        {
            User deserializedUser = (User)serializer.Deserialize(sr);
            Console.WriteLine($"Name: {deserializedUser.Name}");
            // Output: Name: John Doe
        }
    }
}

XML Serialization to File
csharpvar user = new User
{
    Id = 123,
    Name = "John Doe",
    Email = "john@example.com",
    IsActive = true
};

XmlSerializer serializer = new XmlSerializer(typeof(User));

// Write to file
using (FileStream fs = new FileStream("user.xml", FileMode.Create))
{
    serializer.Serialize(fs, user);
}

// Read from file
using (FileStream fs = new FileStream("user.xml", FileMode.Open))
{
    User loadedUser = (User)serializer.Deserialize(fs);
    Console.WriteLine($"Loaded: {loadedUser.Name}");
}

XML with Collections
csharppublic class UserList
{
    [XmlArray("Users")]  // Wrapping element
    [XmlArrayItem("User")]  // Individual item element
    public List<User> Users { get; set; }
}

public class User
{
    [XmlElement("Id")]
    public int Id { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        var userList = new UserList
        {
            Users = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Jane" }
            }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(UserList));
        
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, userList);
            Console.WriteLine(sw.ToString());
        }

        /* Output:
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
        */
    }
}

Complex XML Structure
csharp[XmlRoot("Company")]
public class Company
{
    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlElement("Department")]
    public List<Department> Departments { get; set; }
}

public class Department
{
    [XmlAttribute("Name")]
    public string Name { get; set; }

    [XmlElement("Employee")]
    public List<Employee> Employees { get; set; }
}

public class Employee
{
    [XmlElement("Id")]
    public int Id { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Position")]
    public string Position { get; set; }
}

class Program
{
    static void Main()
    {
        var company = new Company
        {
            Name = "Tech Corp",
            Departments = new List<Department>
            {
                new Department
                {
                    Name = "Engineering",
                    Employees = new List<Employee>
                    {
                        new Employee { Id = 1, Name = "John", Position = "Developer" },
                        new Employee { Id = 2, Name = "Jane", Position = "Manager" }
                    }
                }
            }
        };

        XmlSerializer serializer = new XmlSerializer(typeof(Company));
        
        using (StringWriter sw = new StringWriter())
        {
            serializer.Serialize(sw, company);
            Console.WriteLine(sw.ToString());
        }

        /* Output:
        <?xml version="1.0" encoding="utf-16"?>
        <Company Name="Tech Corp">
          <Department Name="Engineering">
            <Employee>
              <Id>1</Id>
              <Name>John</Name>
              <Position>Developer</Position>
            </Employee>
            <Employee>
              <Id>2</Id>
              <Name>Jane</Name>
              <Position>Manager</Position>
            </Employee>
          </Department>
        </Company>
        */
    }
}

XML Attributes and Options
csharp[XmlRoot("Person")]
public class Person
{
    // As XML attribute
    [XmlAttribute("id")]
    public int Id { get; set; }

    // As XML element
    [XmlElement("FirstName")]
    public string FirstName { get; set; }

    // Ignore this property
    [XmlIgnore]
    public string Password { get; set; }

    // Text content (not an element)
    [XmlText]
    public string Description { get; set; }

    // Namespace
    [XmlElement(Namespace = "http://example.com/person")]
    public string Email { get; set; }
}

Handling XML Namespaces
csharp[XmlRoot("User", Namespace = "http://example.com/users")]
public class User
{
    [XmlElement("Id", Namespace = "http://example.com/users")]
    public int Id { get; set; }

    [XmlElement("Name", Namespace = "http://example.com/users")]
    public string Name { get; set; }
}

var user = new User { Id = 123, Name = "John" };

// Add namespace prefixes
XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
namespaces.Add("usr", "http://example.com/users");

XmlSerializer serializer = new XmlSerializer(typeof(User));

using (StringWriter sw = new StringWriter())
{
    serializer.Serialize(sw, user, namespaces);
    Console.WriteLine(sw.ToString());
}

/* Output:
<?xml version="1.0" encoding="utf-16"?>
<usr:User xmlns:usr="http://example.com/users">
  <usr:Id>123</usr:Id>
  <usr:Name>John</usr:Name>
</usr:User>
*/

JSON vs XML Serialization Comparison
Size Comparison
Same data in both formats:
JSON:
json{
  "Id": 123,
  "Name": "John Doe",
  "Email": "john@example.com",
  "IsActive": true
}
Size: ~85 bytes
XML:
xml<?xml version="1.0" encoding="utf-16"?>
<User IsActive="true">
  <Id>123</Id>
  <Name>John Doe</Name>
  <Email>john@example.com</Email>
</User>
Size: ~165 bytes

Performance Comparison Example
csharpusing System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

class Program
{
    static void Main()
    {
        var user = new User { Id = 123, Name = "John Doe", Email = "john@example.com" };
        int iterations = 100000;

        // JSON Performance Test
        var jsonWatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            string json = JsonSerializer.Serialize(user);
            User deserialized = JsonSerializer.Deserialize<User>(json);
        }
        jsonWatch.Stop();

        // XML Performance Test
        var xmlWatch = Stopwatch.StartNew();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
        for (int i = 0; i < iterations; i++)
        {
            using (StringWriter sw = new StringWriter())
            {
                xmlSerializer.Serialize(sw, user);
                string xml = sw.ToString();
                
                using (StringReader sr = new StringReader(xml))
                {
                    User deserialized = (User)xmlSerializer.Deserialize(sr);
                }
            }
        }
        xmlWatch.Stop();

        Console.WriteLine($"JSON Time: {jsonWatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"XML Time: {xmlWatch.ElapsedMilliseconds}ms");
        
        // Typical result: JSON is 2-3x faster
    }
}

Error Handling
JSON Error Handling
csharpstring invalidJson = "{\"Id\":123,\"Name\":\"John\",}";  // Extra comma - invalid!

try
{
    User user = JsonSerializer.Deserialize<User>(invalidJson);
}
catch (JsonException ex)
{
    Console.WriteLine($"JSON Error: {ex.Message}");
    // Output: JSON Error: ',' is invalid after a property value...
}
XML Error Handling
csharpstring invalidXml = "<User><Id>123</Id><Name>John</User>";  // Unclosed Name tag

try
{
    XmlSerializer serializer = new XmlSerializer(typeof(User));
    using (StringReader sr = new StringReader(invalidXml))
    {
        User user = (User)serializer.Deserialize(sr);
    }
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"XML Error: {ex.Message}");
    Console.WriteLine($"Inner: {ex.InnerException?.Message}");
}

Best Practices
JSON Best Practices
csharp// ✅ Good: Use System.Text.Json for new projects
var options = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true  // Only for debugging
};

// ✅ Good: Handle null values
if (jsonString != null)
{
    var user = JsonSerializer.Deserialize<User>(jsonString);
}

// ✅ Good: Use async for file I/O
await using var fs = File.OpenRead("data.json");
var data = await JsonSerializer.DeserializeAsync<User>(fs);
XML Best Practices
csharp// ✅ Good: Reuse XmlSerializer instances
private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(User));

// ✅ Good: Use using statements for proper disposal
using (var writer = new StreamWriter("data.xml"))
{
    _serializer.Serialize(writer, user);
}

// ✅ Good: Handle exceptions
try
{
    using (var reader = new StreamReader("data.xml"))
    {
        var user = (User)_serializer.Deserialize(reader);
    }
}
catch (InvalidOperationException ex)
{
    // Handle serialization errors
}

Summary
JSON is the modern standard:

Smaller size
Faster performance
Better for web APIs and JavaScript
Human-readable
Use System.Text.Json for new C# projects

XML is still relevant for:

Legacy systems
Enterprise applications
SOAP services
Complex document structures
When attributes and namespaces are needed

Both have robust support in C# and the choice depends on your specific requirements and existing infrastructure!