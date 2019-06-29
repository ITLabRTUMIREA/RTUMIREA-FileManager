# Site for file managing
## Configuration
You must add `appsettings.Secret.json` into FileManager folder with your configuration.
#### Example
```json
{
  "EmailSendingOptions": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "UseSSL": false,
    "Email": "mail@gmail.com",
    "Password": "password"

  }
}
```