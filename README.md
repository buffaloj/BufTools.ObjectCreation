# Object Mother


example:
```cs
var browser = new Browser<Program>(c =>
            {
                c.UseDependency(_userServiceMock.Object);
                c.UseDependency(_validationMock.Object);
            });

var result = await browser.CreateRequest("/api/v1/example")
                          .WithQueryParam("string_param", myString)
                          .WithQueryParam("int_param", myInt)
                          .GetAsync();
```

# Getting Started
