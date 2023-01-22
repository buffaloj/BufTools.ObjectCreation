# Object Mother
The ObjectMother class will create an instance of an object and initialize the properties from the example value provided by the XML comments on the property.

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

# Getting Started

1. The ObjectMother uses an IServiceProvider to create an instance of a class. If dependency injection is not available and/or an existing IServiceProvider is not available, one can be constructed like this:

```cs
var sc = new ServiceCollection();
sc.AddSingleton<ExampleModel>();
var provider = sc.BuildServiceProvider();
```
* BuildServiceProvider is located in the Microsoft.Extensions.DependencyInjection nuget package

2. If not using DependencyInjection, then create an ObjectMother instance:
```cs
var mother = new ObjectMother(provider);
```

3. Use the instance to create an object:
```cs
var instance = _target.Birth<ExampleModel>();
```

