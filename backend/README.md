# PhoneBook API - Backend

## Overview
This is the backend service for the PhoneBook application, built using **.NET Core**. It provides a set of RESTful APIs for managing user accounts and contacts.

## Technologies Used
- **.NET Core 8**
- **Entity Framework Core** (EF Core) for database access
- **Swagger** for API documentation
- **SQL Server** for data storage
- **AutoMapper** for object mapping
- **Dependency Injection (DI)** for service management
- **JWT Authentication** for securing endpoints

## Project Structure (Onion Archticture)
```
PhonebookAPI/
│── phoneBook.DataAccess/       # Data access layer
│   ├── ApplicationContext/     # Database context
│   ├── Extensions/             # Extension methods
│   ├── Migrations/             # EF Core migrations
│   ├── Repository/             # Repository layer
│   ├── Services/               # Business logic services
│
│── phoneBook.Entities/         # Core domain entities
│   ├── DTO/                    # Data Transfer Objects
│   ├── Exceptions/              # Custom exceptions
│   ├── IRepository/             # Repository interfaces
│   ├── Models/                  # Database models
│
│── Phonebook.Presentation/     # Presentation layer
│   ├── Controllers/            # API controllers
│   ├── Profiles/               # AutoMapper profiles
│   ├── appsettings.json        # Configuration file
│   ├── Program.cs              # Application entry point
```

## API Endpoints
### Account
| Method | Endpoint               | Description           |
|--------|------------------------|-----------------------|
| POST   | `/api/Account/Register` | Register a new user  |
| POST   | `/api/Account/Login`    | Authenticate user    |

### Contacts
| Method | Endpoint                            | Description                 |
|--------|------------------------------------|-----------------------------|
| GET    | `/api/Contact/GetAllContacts`      | Retrieve all contacts       |
| GET    | `/api/Contact/GetContactById/{id}` | Get contact by ID           |
| GET    | `/api/Contact/GetContactByName/{name}` | Search contact by name |
| POST   | `/api/Contact/AddNewContact`       | Create a new contact        |
| PUT    | `/api/Contact/{id}`                | Update a contact            |
| DELETE | `/api/Contact/{id}`                | Delete a contact            |

## Setup Instructions
### 1. Clone the Repository
```sh
git clone https://github.com/your-repo.git
cd PhonebookAPI
```

### 2. Configure the Database
Modify `appsettings.json` with your database connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=PhoneBookDB;Trusted_Connection=True;"
}
```

### 3. Apply Migrations
```sh
dotnet ef database update
```

### 4. Run the Application
```sh
dotnet run --project Phonebook.Presentation
```

### 5. Access Swagger API Documentation
Once running, navigate to:
```
http://localhost:5000/swagger/index.html
```

## Authentication
- All secured endpoints require **JWT Authentication**.
- Obtain a token via the `/api/Account/Login` endpoint and include it in requests as:
  ```sh
  Authorization: Bearer YOUR_ACCESS_TOKEN
  ```
