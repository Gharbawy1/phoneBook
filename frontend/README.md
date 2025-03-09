# 📱 Phonebook Frontend

This is the **frontend** of the Phonebook application built using **Angular 19**. It allows **admins** to manage contacts and supports user authentication. 🎯

## 🌟 Features

✅ **Login & Authentication**  
✅ **Contact Management** (Add, Edit, Delete, Update)  
✅ **Live Search** 🔍  
✅ **Pagination** 🗄  
✅ **Role-Based Access Control** (Users can only view contacts)  

---

## 🛠️ Installation & Setup

1. Clone the repository:  
   ```sh
   git clone https://github.com/your-repo.git
   cd frontend
   ```

2. Install dependencies:  
   ```sh
   npm install
   ```

3. Start the development server:  
   ```sh
   ng serve
   ```
   The app will be available at **`http://localhost:4200/`** 🚀  

---

## 🔑 Authentication & Roles

- **Admin:** Can **Add, Edit, Delete, and Update** contacts.  
- **User:** Can **only view** contacts.  

---

## 📌 API Endpoints

The frontend interacts with the **backend API** for authentication and contact management. Below are the key endpoints used:

### 🟢 **Authentication**
- `POST /api/Account/Register` ➔ Register a new user  
- `POST /api/Account/Login` ➔ User login  

### 👇 **Contacts**
- `GET /api/Contact/GetAllContacts` ➔ Fetch all contacts  
- `GET /api/Contact/GetContactById/{id}` ➔ Get a contact by ID  
- `POST /api/Contact/AddNewContact` ➔ Add a new contact  
- `PUT /api/Contact/{id}` ➔ Update a contact  
- `DELETE /api/Contact/{id}` ➔ Delete a contact  

---

## 🎨 UI Components

- **Login Page**: Allows users to log in. 🔑  
- **Dashboard**: Displays all contacts with pagination. 👇  
- **Live Search**: Instantly search for contacts. 🔍  
- **Admin Controls**: Edit, Delete, and Add contacts. ⚙️  

---

## 🏗️ Tech Stack

- **Angular 19** 🅰️  
- **TypeScript** ⌨️  
- **Angular Material** 🎨  
- **RxJS** 🔄  

---
