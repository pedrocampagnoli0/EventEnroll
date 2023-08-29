# About
This is a .NET REST API with the basic CRUD for the Event class
- Identity Framework, for managing the users
- Entity for the database management
- JWT for the authentication
- AutoMapper for mapping the objects

# Requisites
- .NET version 7.0 or superior
- SQL Server Express

# Installation
```bash
git clone https://github.com/pedrocampagnoli0/EventEnroll.git
```
# Installing the packages
```bash
dotnet add package AutoMapper
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.OpenApi
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Swashbuckle.AspNetCore
dotnet add package Swashbuckle.AspNetCore.Filters
```

# Usage
Change the "DefaultConnection", to your local SQL Server
```json
"ConnectionStrings": {
  "DefaultConnection": "REPLACEYOURSERVERHERE; Database=EventEnroll; Trusted_Connection=true; TrustServerCertificate=true;"
},
```
Updates the database to your current SQL Server
```bash
dotnet ef database update
```
```bash
dotnet build
```
```bash
dotnet run
```

# AuthController

Handles user authentication, including registration and login.
POST /Auth/Register

- Registers a new user.
- Request Format (JSON):
```json
{
  "UserName": "exampleUser",
  "Password": "securePassword"
}
```
Response Format (JSON):
```json
{
  "data": "UserId",
  "success": true,
  "message": ""
}
```
POST /Auth/Login
- Logs in a user.
- Request Format (JSON):
```json
{
  "UserName": "exampleUser",
  "Password": "securePassword"
}
```
Response Format (JSON):
```json
{
  "data": "JWT Token",
  "success": true,
  "message": ""
}
```

# EventController
Handles the management of events, including creation, retrieval, updating, and deletion.

GET /Event
- Retrieves all events created by the authenticated user.
- Response Format (JSON):
```json
{
  "data": [
    {
      "eventId": 1,
      "title": "Event Title",
      "description": "Event Description",
      "date": "dd-MM-yyyy",
      "creator": {
        "id": "UserId",
        "userName": "Username"
      },
      "attendees": [
        {
          "id": "UserId",
          "userName": "Username"
        }
      ]
    },
    ...
  ],
  "success": true,
  "message": ""
}
```
GET /Event/{id}
- Retrieves details of a specific event created by the authenticated user.
- Response Format, same as above

POST /Event
- Adds a new event created by the authenticated user.
- Request Format (JSON):
```json
{
  "title": "Event Title",
  "description": "Event Description",
  "date": "dd-MM-yyyy",
  "attendees": ["Username1", "Username2"]
}
```
Response Format (JSON):
```json
{
  "data": [
    {
      "eventId": 1,
      "title": "Event Title",
      "description": "Event Description",
      "date": "dd-MM-yyyy",
      "creator": {
        "id": "UserId",
        "userName": "Username"
      },
      "attendees": [
        {
          "id": "UserId",
          "userName": "Username"
        }
      ]
    },
    ...
  ],
  "success": true,
  "message": ""
}
```
PUT /Event/{id}
Updates details of a specific event created by the authenticated user.
Request Format (JSON):
```json
{
  "title": "Updated Title",
  "description": "Updated Description",
  "date": "dd-MM-yyyy",
  "attendees": ["NewUsername"]
}
```
Response Format (JSON):
```json
{
  "data": [
    {
      "eventId": 1,
      "title": "Event Title",
      "description": "Event Description",
      "date": "dd-MM-yyyy",
      "creator": {
        "id": "UserId",
        "userName": "Username"
      },
      "attendees": [
        {
          "id": "UserId",
          "userName": "Username"
        }
      ]
    },
    ...
  ],
  "success": true,
  "message": ""
}
```
DELETE /Event/{id}
- Deletes a specific event created by the authenticated user.
- Response Format (JSON):
```json
{
  "data": [
    {
      "eventId": 2,
      "title": "Another Event",
      ...
    },
    ...
  ],
  "success": true,
  "message": ""
}
```



