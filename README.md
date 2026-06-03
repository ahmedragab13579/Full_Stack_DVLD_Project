# 🚗 Full Stack DVLD Project
## Driving License Application Management System

A comprehensive **Full-Stack Web Application** for managing driving license applications, tests, appointments, and license issuance. Built with modern technologies and best practices for scalability and maintainability.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Architecture](#project-architecture)
- [Installation & Setup](#installation--setup)
- [Project Structure](#project-structure)
- [Key Features in Detail](#key-features-in-detail)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Screenshots & UI](#screenshots--ui)
- [User Roles & Permissions](#user-roles--permissions)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

---

## 🎯 Overview

The **Full Stack DVLD Project** is a complete web-based solution for managing driving license operations. It streamlines the entire lifecycle of license applications, from initial application creation through test scheduling, result recording, and final license issuance. The system includes advanced features for user management, license detention/release, and comprehensive reporting.

**Project Created:** November 14, 2025  
**Last Updated:** May 10, 2026  
**Repository:** [GitHub Link](https://github.com/ahmedragab13579/Full_Stack_DVLD_Project)

---

## ✨ Features

### Core Functionality
- ✅ **License Application Management** - Create, track, and manage driver license applications
- ✅ **Test Scheduling & Management** - Schedule and record driving tests with results
- ✅ **Appointment System** - Book and manage appointments for applications and tests
- ✅ **License Issuance** - Process and issue driving licenses upon successful completion
- ✅ **License Detention** - Track and manage detained/suspended licenses
- ✅ **License Release** - Process license release with fine fees management

### Administrative Features
- ✅ **User Management** - Create and manage system users with role-based access
- ✅ **License Class Management** - Maintain different types of license categories
- ✅ **System Reports** - Generate comprehensive reports on applications, tests, and licenses
- ✅ **Person Management** - Maintain person profiles and driver information
- ✅ **Driver Directory** - Central directory of all system drivers

### System Features
- ✅ **Role-Based Access Control** - Administrator, Officer, Clerk, and Viewer roles
- ✅ **Module-Level Permissions** - Fine-grained permission management
- ✅ **Audit Trail** - Track creation and modification of records
- ✅ **Soft Delete** - Safe data deletion with recovery options
- ✅ **Real-time Validation** - Client and server-side form validation
- ✅ **Responsive Design** - Mobile-friendly interface with Tailwind CSS

---

## 🛠 Technology Stack

### Backend
- **Framework:** ASP.NET Core (C# 61.1%)
- **Architecture:** Layered Architecture with Domain-Driven Design
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **API:** RESTful APIs with MVC pattern

### Frontend
- **View Engine:** Razor Pages/Cshtml (HTML 34.9%)
- **Styling:** Tailwind CSS with custom DVLD theme
- **JavaScript:** Vanilla JS (JavaScript 2.6%) + jQuery for interactions
- **CSS:** Custom styling (CSS 1.4%)
- **Icons:** SVG-based icons for consistency

### Architecture Layers
1. **Presentation Layer** - ASP.NET Core MVC with Razor Views
2. **Application Layer** - Business logic and DTOs
3. **Domain Layer** - Core entities and base classes
4. **Persistence Layer** - Entity Framework Core with SQL Server

---

## 🏗 Project Architecture

```
Full_Stack_DVLD_Project/
├── DVLD_Domain/              # Domain layer - Core business entities
│   └── Models/
│       ├── BaseEntity.cs      # Base class with audit info
│       ├── User.cs
│       ├── Person.cs
│       ├── Application.cs
│       ├── Test.cs
│       ├── Appointment.cs
│       ├── License.cs
│       ├── DetainedLicense.cs
│       └── LicenseClass.cs
│
├── DVLD_Application/          # Application layer - Business logic
│   ├── Services/
│   │   ├── Interfaces/
│   │   │   ├── IApplicationService.cs
│   │   │   ├── ITestService.cs
│   │   │   ├── ILicenseService.cs
│   │   │   └── IDetainLicenseService.cs
│   │   └── Implementations/
│   └── Dtos/
│       ├── AddDtos/           # Creation DTOs
│       ├── UpdateDtos/        # Update DTOs
│       └── ReadDtos/          # Read/response DTOs
│
├── DVLD_Persistence/          # Data access layer
│   ├── DbContext/
│   └── Repositories/
│
├── DVLD_Persentation/         # Presentation layer
│   ├── Controllers/
│   │   ├── HomeController.cs
│   │   ├── ApplicationController.cs
│   │   ├── TestController.cs
│   │   ├── AppointmentController.cs
│   │   ├── LicenseController.cs
│   │   ├── DetainLicenseController.cs
│   │   ├── UserController.cs
│   │   ├── PersonController.cs
│   │   ├── DriverController.cs
│   │   └── LicenseClassController.cs
│   │
│   ├── Views/
│   │   ├── Shared/
│   │   │   ├── _Layout.cshtml       # Main layout
│   │   │   ├── _AuthLayout.cshtml   # Auth layout
│   │   │   ├── _Alerts.cshtml
│   │   │   ├── _Breadcrumbs.cshtml
│   │   │   └── _ObjectTable.cshtml
│   │   ├── Application/
│   │   ├── Test/
│   │   ├── Appointment/
│   │   ├── License/
│   │   ├── DetainLicense/
│   │   ├── User/
│   │   ├── Person/
│   │   ├── Driver/
│   │   └── LicenseClass/
│   │
│   ├── wwwroot/                # Static files
│   │   ├── css/
│   │   ├── js/
│   │   └── images/
│   │
│   └── appsettings.json
```

---

## 📥 Installation & Setup

### Prerequisites
- **.NET 6.0** or higher
- **SQL Server** (LocalDB or full version)
- **Visual Studio 2022** or VS Code
- **Git**

### Step 1: Clone the Repository
```bash
git clone https://github.com/ahmedragab13579/Full_Stack_DVLD_Project.git
cd Full_Stack_DVLD_Project
```

### Step 2: Configure Database Connection
Edit `DVLD_Persentation/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DVLD;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Step 3: Restore NuGet Packages
```bash
dotnet restore
```

### Step 4: Create & Seed Database
```bash
dotnet ef database update --project DVLD_Persistence
```

### Step 5: Build the Solution
```bash
dotnet build
```

### Step 6: Run the Application
```bash
cd DVLD_Persentation
dotnet run
```

The application will be available at: `https://localhost:5001`

---

## 📂 Project Structure

### Layer Details

#### 1. **DVLD_Domain** (Core Business Logic)
Contains all domain entities with base audit functionality:

```csharp
public class BaseEntity
{
    public int Id { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int? CreatedByUserId { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public int? UpdatedByUserId { get; private set; }
}
```

#### 2. **DVLD_Application** (Business Logic & DTOs)
Service interfaces and DTOs for application operations:
- `IApplicationService` - Application management
- `ITestService` - Test scheduling and results
- `ILicenseService` - License operations
- `IDetainLicenseService` - License detention/release

#### 3. **DVLD_Persentation** (ASP.NET Core MVC)
Controllers and Views with Razor templating and Tailwind CSS styling.

---

## 🎨 Key Features in Detail

### 1. Dashboard
- **Overview of system metrics**
- **Quick access to core modules**
- **Recent activity tracking**

![Dashboard Screenshot](https://via.placeholder.com/1000x600?text=Dashboard)

### 2. License Application Management
- **Multi-step wizard** for creating new applications
- **Application status tracking** (New, Pending, Approved, Rejected, Issued)
- **Applicant information validation**
- **Application history and details**

![Application Creation](https://via.placeholder.com/1000x600?text=Application+Creation)
![Application List](https://via.placeholder.com/1000x600?text=Application+List)

### 3. Test Management
- **Schedule driving tests** with appointment slots
- **Record test results** (Pass/Fail)
- **Test result tracking** per applicant
- **Multiple test types** support

![Test Scheduling](https://via.placeholder.com/1000x600?text=Test+Scheduling)
![Test Results](https://via.placeholder.com/1000x600?text=Test+Results)

### 4. Appointment System
- **Book appointments** for applications and tests
- **Time slot management**
- **Appointment confirmation and cancellation**
- **Appointment history**

![Appointment Booking](https://via.placeholder.com/1000x600?text=Appointment+Booking)

### 5. License Issuance
- **Issue licenses** upon successful test completion
- **Multiple license classes** support
- **License validity period** management
- **Renewal tracking**

![License Issuance](https://via.placeholder.com/1000x600?text=License+Issuance)

### 6. License Detention & Release
- **Detain licenses** for violations
- **Fine fees management**
- **Release detention** with fee payment confirmation
- **Detention history tracking**

![License Detention](https://via.placeholder.com/1000x600?text=License+Detention)
![License Release](https://via.placeholder.com/1000x600?text=License+Release)

### 7. User Management
- **Create system users** with authentication
- **Assign roles** (Administrator, Officer, Clerk, Viewer)
- **Permission management** per module
- **User activity tracking**

![User Management](https://via.placeholder.com/1000x600?text=User+Management)
![User Edit](https://via.placeholder.com/1000x600?text=User+Edit+Form)

### 8. Reports
- **Application statistics**
- **Test results analysis**
- **License issuance reports**
- **System usage analytics**

![Reports Dashboard](https://via.placeholder.com/1000x600?text=Reports+Dashboard)

### 9. Person Management
- **Maintain person profiles**
- **Driver information**
- **Contact details**
- **Document tracking**

![Person Directory](https://via.placeholder.com/1000x600?text=Person+Directory)

### 10. Driver Directory
- **Central driver database**
- **Search and filter drivers**
- **License status overview**
- **Driver history**

![Driver Directory](https://via.placeholder.com/1000x600?text=Driver+Directory)

---

## 📊 Database Schema

### Core Entities

**User**
- UserId (PK)
- UserName
- Email
- FullName
- Role
- IsActive
- Audit Fields

**Person**
- PersonId (PK)
- FirstName, MiddleName, LastName
- NationalNo
- DateOfBirth
- Gender
- Address
- Phone, Email

**Application**
- ApplicationId (PK)
- ApplicantPersonId (FK)
- ApplicationDate
- ApplicationTypeId (FK)
- ApplicationStatus
- LastStatusUpdateDate
- CreatedByUserId (FK)

**License**
- LicenseId (PK)
- ApplicationId (FK)
- DriverId (FK)
- LicenseClassId (FK)
- IssueDate, ExpirationDate
- IsActive

**Test**
- TestId (PK)
- ApplicationId (FK)
- TestTypeId (FK)
- TestDate
- TestResult (Pass/Fail)
- Notes

**Appointment**
- AppointmentId (PK)
- ApplicationId (FK)
- AppointmentDate
- AppointmentStatus

**DetainedLicense**
- DetainId (PK)
- LicenseId (FK)
- DetainDate
- FineFees
- IsReleased
- ReleaseDate

**LicenseClass**
- ClassId (PK)
- ClassName
- MinimumAge
- DefaultValidityLength

---

## 🔌 API Endpoints

### Application Endpoints
- `GET /Application/LocalIndex` - List all applications
- `GET /Application/Details/{id}` - Get application details
- `GET /Application/Create` - Create new application (form)
- `POST /Application/Create` - Submit new application
- `GET /Application/Edit/{id}` - Edit application (form)
- `POST /Application/Edit/{id}` - Update application

### Test Endpoints
- `GET /Test/Index` - List all tests
- `GET /Test/Details/{id}` - Get test details
- `GET /Test/Create` - Create test appointment (form)
- `POST /Test/Create` - Submit test creation
- `GET /Test/UpdateResult/{id}` - Update test result (form)
- `POST /Test/UpdateResult/{id}` - Submit test result

### License Endpoints
- `GET /License/Index` - List all licenses
- `GET /License/Details/{id}` - Get license details
- `GET /License/Renew/{id}` - Renew license

### Detain License Endpoints
- `GET /DetainLicense/Index` - List detained licenses
- `GET /DetainLicense/Details/{id}` - Get detention details
- `GET /DetainLicense/Detain` - Detain license (form)
- `POST /DetainLicense/Detain` - Submit detention
- `GET /DetainLicense/Release` - Release license (form)
- `POST /DetainLicense/Release` - Submit release

### User Endpoints
- `GET /User/Index` - List users
- `GET /User/Create` - Create user (form)
- `POST /User/Create` - Submit user creation
- `GET /User/Edit/{id}` - Edit user (form)
- `POST /User/Edit/{id}` - Update user

---

## 📸 Screenshots & UI

### Application Interface

**Dashboard & Navigation**
![Screenshot Placeholder 1](https://via.placeholder.com/1000x600?text=Dashboard+Navigation)

**Application Wizard - Step 1**
![Screenshot Placeholder 2](https://via.placeholder.com/1000x600?text=Application+Wizard+Step+1)

**Application Wizard - Step 2**
![Screenshot Placeholder 3](https://via.placeholder.com/1000x600?text=Application+Wizard+Step+2)

**Application Wizard - Step 3**
![Screenshot Placeholder 4](https://via.placeholder.com/1000x600?text=Application+Wizard+Step+3)

**Test Management Interface**
![Screenshot Placeholder 5](https://via.placeholder.com/1000x600?text=Test+Management)

**Appointment Booking**
![Screenshot Placeholder 6](https://via.placeholder.com/1000x600?text=Appointment+Booking)

**License Details View**
![Screenshot Placeholder 7](https://via.placeholder.com/1000x600?text=License+Details)

**License Detention Form**
![Screenshot Placeholder 8](https://via.placeholder.com/1000x600?text=License+Detention)

**License Release Process**
![Screenshot Placeholder 9](https://via.placeholder.com/1000x600?text=License+Release)

**User Management Panel**
![Screenshot Placeholder 10](https://via.placeholder.com/1000x600?text=User+Management)

**Permission Assignment**
![Screenshot Placeholder 11](https://via.placeholder.com/1000x600?text=Permission+Assignment)

**Reports Dashboard**
![Screenshot Placeholder 12](https://via.placeholder.com/1000x600?text=Reports+Dashboard)

**Person Directory**
![Screenshot Placeholder 13](https://via.placeholder.com/1000x600?text=Person+Directory)

**Driver Directory**
![Screenshot Placeholder 14](https://via.placeholder.com/1000x600?text=Driver+Directory)

---

## 👥 User Roles & Permissions

### Role Hierarchy

#### 1. **Administrator**
- Full system access
- User management
- System configuration
- All module permissions
- Report generation
- System audits

#### 2. **License Officer**
- Application review and approval
- Test scheduling and supervision
- License issuance
- Detention/release authority
- Limited reporting

#### 3. **Data Entry Clerk**
- Create and edit person records
- Enter test results
- Create applications
- Appointment scheduling
- Read-only reports

#### 4. **Read-Only Viewer**
- View all data
- Report viewing
- No modification rights
- Audit trail access

### Module Permissions
- Person Management
- Applications
- Appointments
- Test Records
- License Issuance
- Detain Licenses
- User Management
- License Classes
- System Reports

---

## ⚙️ Configuration

### Application Settings (`appsettings.json`)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=DVLD;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "AllowedHosts": "*"
}
```

### Tailwind CSS Configuration
The project uses a custom Tailwind configuration with DVLD branding:
- **Primary Color (Teal):** `#00ADB5`
- **Dark Background:** `#1a1e23`
- **Gray Secondary:** `#2d333b`
- **Light Text:** `#EEEEEE`

### View Engine Settings
- Razor Pages for authentication views
- Razor components for shared layouts
- Strongly-typed models for all views
- Validation helpers and tag helpers

---

## 🌟 Design & UI Principles

### Color Scheme
| Color | Hex | Usage |
|-------|-----|-------|
| Primary Teal | #00ADB5 | Buttons, accents, active states |
| Dark Background | #1a1e23 | Main background |
| Gray | #2d333b | Cards, secondary backgrounds |
| Light Text | #EEEEEE | Primary text |
| Error Red | #FF6B6B | Error messages |

### Typography
- **Font Family:** Inter (Google Fonts)
- **Headings:** Bold weight (700)
- **Body Text:** Regular weight (400)
- **Small Text:** Light weight (300)

### Component Standards
- **Input Fields:** Full width on mobile, constrained on desktop
- **Error Messages:** Red color with validation indicators
- **Success Messages:** Teal color with check icon
- **Buttons:** Hover and active states clearly defined
- **Forms:** Real-time validation feedback

---

## 🤝 Contributing

### How to Contribute
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow C# coding conventions (PascalCase for public members)
- Write meaningful commit messages
- Add comments for complex logic
- Test all changes before submitting PR
- Keep layers separated and responsibilities clear

---

## 📝 License

This project is currently **unlicensed**. Please check the repository for licensing information.

---

## 📞 Contact & Support

**Author:** [Ahmed Ragab](https://github.com/ahmedragab13579)  
**Repository:** [Full_Stack_DVLD_Project](https://github.com/ahmedragab13579/Full_Stack_DVLD_Project)

For issues, questions, or suggestions, please open an issue on GitHub.

---

## 📈 Project Statistics

- **Language Composition:**
  - C# - 61.1%
  - HTML - 34.9%
  - JavaScript - 2.6%
  - CSS - 1.4%

- **Repository Size:** ~6.3 MB
- **Created:** November 14, 2025
- **Last Updated:** May 10, 2026
- **Open Issues:** 0
- **Watchers:** 1

---

## 🎓 Learning Resources

This project demonstrates:
- ✅ Full-stack web application development
- ✅ Layered architecture patterns
- ✅ Domain-Driven Design principles
- ✅ Entity Framework Core with SQL Server
- ✅ ASP.NET Core MVC best practices
- ✅ Responsive UI with Tailwind CSS
- ✅ Role-based access control
- ✅ Audit trail implementation
- ✅ Advanced form validation
- ✅ RESTful API design

---

## 🚀 Future Enhancements

Potential improvements for future versions:
- [ ] API documentation with Swagger/OpenAPI
- [ ] Unit and integration testing framework
- [ ] Automated email notifications
- [ ] SMS alerts for appointments
- [ ] Mobile application
- [ ] Advanced analytics dashboard
- [ ] Payment gateway integration
- [ ] Multi-language support (i18n)
- [ ] Two-factor authentication
- [ ] Audit log viewer UI

---

<div align="center">

**Made with ❤️ by Ahmed Ragab**

⭐ If you find this project helpful, please consider giving it a star!

</div>
