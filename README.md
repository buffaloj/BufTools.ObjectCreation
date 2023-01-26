# Object Mother

This solution provides an ObjectMother class will create an instance of an object and initialize it from the example values in your XML comments.

The same <example> values are used by Swagger to populate object examples (after providing the XML files to Swagger).

example:
```cs
public class ExampleModel
{
	/// <summary>
	/// An example of a string property
	/// </summary>
	/// <example>A String!</example>
	public string StringProperty { get; set; }
}

var instance = objectMother.Birth<ExampleModel>();
```
* Results in (instance.StringProperty == "A String!")

# Getting Started

The simplest use case for the ObjectMother is to new it up and use it:
```cs
var mother = new ObjectMother();
var instance = _target.Birth<ExampleModel>();
```

## Ignoring Properties

If you have properties that you don't want populated from XML comments, then you can add an [Attribute] to the property.  Then tell the ObjectMother what attributes you want to ignore.

```cs
public class ModelWithIgnore
{
	[JsonIgnore]
	public string Ignored { get; set; }

	/// <summary>
	/// An example int
	/// </summary>
	/// <example>99</example>
	public int IntExample { get; set; }
}
	
var mother = new ObjectMother();
mother.IgnorePropertiesWithAttribute<JsonIgnoreAttribute>();
var instance = _target.Birth<ModelWithIgnore>();
```

## Dependency Injection
The ObjectMother uses Activator to create the object.  This is fine for creating any object with a parameterless constructor.  If such a constructor is not available, then a IServiceProvider instance is needed.

* If an IServiceProvider is provided and it fails to create the requested object, then Activator is then used.

1. You may alredy have a service provider available.  If not, one can be constructed:

```cs
var sc = new ServiceCollection();
sc.AddSingleton<ExampleModel>();
var provider = sc.BuildServiceProvider();
```
* BuildServiceProvider is located in the Microsoft.Extensions.DependencyInjection nuget package

2. Create an ObjectMother instance:
```cs
var mother = new ObjectMother(provider);
```

3. Use the instance to create an object:

```cs
var instance = _target.Birth<ExampleModel>();
```

