## Configucius

Configucius is a simple netstandard20 based configuration reader.

### NuGet Packages
``` 
dotnet add package Configucius
```

### Features

- It allows you to separate your configurations based on your domain & environment.
- Built-in MSSQL provider. (You can extend it)
- It provides scheduled configuration update.

### Usages
-----
Simple usage of Configucius. 
First, you need to add some parameters in your app.config file as follows:

- "Configucius_ConnectionString" for MSSQL provider.
- "Configucius_Domain" parameter is important to separate your configurations based on your domain.
- "Configucius_Environment" parameter is important to separate your configurations based on your environments.

```cs
IConfigucius Configucius = new ConfiguciusClient(configRepository: new SqlConfigRepository(), refreshTime: TimeSpan.FromMinutes(2));

string key = "Hello!";
string value = Configucius.GetValue<string>(key);
```
