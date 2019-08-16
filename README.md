# Site for file managing
## Configuration
You must add `appsettings.Secret.json` into FileManager folder with your configuration.
#### Example
```json
{
  "EmailSendingOptions": {
    "Host": "smtp.yandex.ru",
    "Port": 465,
    "UseSSL": true,
    "Email": "mail@yandex.ru",
    "Password": "password"

  },
  "DbInitializeMainUser": {
    "Email": "yourEmail@email.com",
    "FirstName": "FirstName",
    "LastName": "LastName",
    "Password": "password"
  }
}
```
#### Additional configuration for Mac OS
1. Run this command in FileManager folder via terminal  `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>'    -p 1433:1433 --name sql1    -d mcr.microsoft.com/mssql/server:2017-latest`
2. Replace this code in Startup.cs
```csharp
            services.AddDbContext<FileManagerContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("FileManagerContext")));
```
To your local ConnectionString.  

 **Important**   
Use Connection string in open form ***only*** for debug, in other ways hide connection string to appsettings.json
```csharp
            services.AddDbContext<FileManagerContext>(options =>
                  options.UseSqlServer("Server=tcp:localhost,1433;Initial Catalog=labwork;Persist Security Info=False;" +
                  "User ID=SA;Password=<YourStrong!Passw0rd>;MultipleActiveResultSets=False;Encrypt=True;" +
                  "TrustServerCertificate= True;Connection Timeout=30;"));
```