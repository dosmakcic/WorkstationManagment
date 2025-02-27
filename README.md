# Workstation Management System

This application is built using **AvaloniaUI** and **ReactiveUI** for cross-platform UI development with MVVM pattern. It integrates with a **MySQL database** using **Entity Framework Core**.

## Features

- ✅ Cross-Platform UI with AvaloniaUI  
- ✅ MVVM Architecture using ReactiveUI  
- ✅ MySQL Database Integration  
- ✅ Dependency Injection with Microsoft.Extensions.DependencyInjection  
- ✅ Entity Framework Core for Data Access  

---

## 📌 Prerequisites

Before running the project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL Server](https://dev.mysql.com/downloads/)
- Visual Studio 2022 (or another .NET-compatible IDE) 
---

## 🛠️ Project architecture

WorkstationManagment.Core     (Business logic,models,services and data) <br />
WorkstationManagment.UI       ( UI (ViewModels and Views  )        <br />           

## 🛠 Database Setup

###  Option A: Running MySQL in a Docker Container  

If you prefer to use **Docker**, follow these steps to set up MySQL:  

1. **Start a MySQL Container**  
   Open a terminal and run the following command:  

   ```sh
   docker run --name workstation_db -e MYSQL_ROOT_PASSWORD=my-secret-pw -e MYSQL_DATABASE=workstation_db -p 3306:3306 -d mysql:latest

This command:

Creates a MySQL container named workstation_db.
Sets the root password to my-secret-pw.
Creates a database named workstation_db.
Exposes MySQL on port 3306.

###  Option B: Using a Local MySQL Database

If you want to use a locally installed MySQL instead of Docker:

1. Install MySQL Server – Download Here.

Create a database manually

2. Connect to MySQL using a MySQL client or terminal:
   ```sh
   mysql -u root -p

3. Then, create the database:
   ```sh
   CREATE DATABASE workstation_db;

Check the Connection String in App.axaml.cs


string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

4. Apply database migrations
  
  dotnet ef database update

If you use Package Manager Console , then use :

   `Update-Database -StartupProject WorkstationManagment.UI -Project WorkstationManagment.Core`


### 📊 Seeded Test Data

The database is automatically seeded with the following test users:

| Username | Password | Role |
| :---      |     :---:    |     ---: |
| john123   | johndoe123   | Admin    |
| marko1    | marko1       | User     |
| ivan1     | ivan1        | User     |
| ana1      | ana1         | User     |

Admin users can access all functionalities, while regular users have limited access.


## 🚀 Running the Application

  
   `dotnet run`

 ## Login view

 ![image](https://github.com/user-attachments/assets/d96a4db3-d927-4d0c-8233-4df2e34ed6db)

After entering the username and password, it is checked whether the user is an admin or an ordinary user.  

 ## Admin view

  If the user is an Admin, a window will open where:
    -He can add new users with work position: 
    ![image](https://github.com/user-attachments/assets/f0748f17-34a1-42b0-80a0-5828052a8fc8)
  
  -He can delete users with work position:
    ![image](https://github.com/user-attachments/assets/c5045d9e-f2e6-4a58-8e7f-55d81185e278)
  
  
   -He can add new work position and change user work position: 
    ![image](https://github.com/user-attachments/assets/d0d68d30-5c7e-45aa-b4ea-700f064fafa7)
  
  -He can view all the users with work position,assigned date, username, first name, last name and product name with logout button at the bottom:
    ![image](https://github.com/user-attachments/assets/7afbbf39-3c58-4de7-a974-36a7ccce743c)

## User view
  If the user is regular user, after login window he can see his user information like first name, last name, work position with description, product name and assigned date (when he got the position) with logout button:
  ![image](https://github.com/user-attachments/assets/13b856bb-b77d-4edd-bb95-463dd5dea216)
   


