## Configucius

Configucius is a simple netstandard20 based configuration reader.

### NuGet Packages
``` 
dotnet add package Configucius
```

### Features

- Built-in MSSQL provider.
- It provides scheduled configuration update.

### Usages
-----
Simple usage of Comfigucius.

```cs
IConfigucius Configucius = new ConfiguciusClient(configRepository: new SqlConfigRepository(), refreshTime: TimeSpan.FromMinutes(2));

string key = "Hello!";
string value = Configucius.GetValue<string>(key);
```