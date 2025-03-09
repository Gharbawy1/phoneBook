# 🚀 Contact Management System  

A full-stack **Contact Management System** built using:
- **📡 Backend:** .NET Core 7 with Entity Framework Core  
- **💻 Frontend:** Angular 17 with Angular Material  

This system allows users to manage contacts efficiently, providing **CRUD operations**, **pagination**, and an intuitive UI.  

---

## 📌 Project Overview  
This project consists of two main components:

1. **Backend (`/backend`)**  
   - RESTful API built with .NET 7  
   - Authentication using JWT  
   - SQL Server database  
   - API documentation using Swagger  

2. **Frontend (`/frontend`)**  
   - Angular 17 for UI  
   - Material UI for styling  
   - Responsive design  
   - Integration with the backend via HTTP requests  

---

## 🛠️ Installation & Setup  

### **1️⃣ Prerequisites**  
Ensure you have the following installed on your system:

- [Node.js (Latest LTS)](https://nodejs.org/)  
- [Angular CLI](https://angular.io/cli)  
- [.NET SDK 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  
- [Docker](https://www.docker.com/) (optional for database containerization)  

---

### **2️⃣ Setting Up the Backend**  
Navigate to the **`backend`** directory and follow these steps:

#### **➡️ Install Dependencies**  
```bash
cd backend
dotnet restore
