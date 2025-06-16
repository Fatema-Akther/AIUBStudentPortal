🎓 AIUB Student Portal
A complete Student Portal Web Application built using ASP.NET MVC with a clean 3-tier architecture. This project was developed as part of an internship evaluation at AIUB (American International University-Bangladesh).


🚀 Overview
The AIUB Student Portal provides a seamless platform for students to register, manage courses, view class schedules, and communicate with faculty. The admin panel allows administrative control over student data and course enrollment.

🏗 Architecture
The application follows a pure 3-tier architecture:

🔹 Presentation Layer (UI) – Built with ASP.NET MVC and Razor Views. Handles all user interactions.

🔹 Business Logic Layer (BLL) – Processes data using services, DTOs, and AutoMapper. No EF or DB logic here.

🔹 Data Access Layer (DAL) – Uses Entity Framework via Repository Pattern for all database interactions.


✨ Features
✅ Student Registration
✅ Admin Login & Secure Session
✅ Course Selection (max 5 per student)
✅ Confirmed Registration Tracking
✅ Class Schedule Grouped by Day
✅ Course Drop Request & Status Update
✅ Teams/Resource Links per Course
✅ Responsive UI with Razor & Bootstrap


🛠 Technologies Used
ASP.NET MVC 5 (.NET Framework 4.8)

Entity Framework (Code First)

C#, LINQ, SQL Server

AutoMapper

Bootstrap / Custom CSS

Razor View Engine

Session Management



📂 Project Structure

AIUBStudentPortal/
│
├── Controllers/
│   └── AdminController.cs
│
├── Models/
│   └── DTOs, Entities
│
├── BLL/
│   └── Services, Interfaces
│
├── DAL/
│   └── Repositories, EF Context
│
├── Views/
│   ├── Admin/
│   └── Student/
│
└── Web.config





