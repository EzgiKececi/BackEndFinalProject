
# FitGoalApp
FitGoalApp is an application built with ASP.NET Core, where users can register, log in, and create their own profiles.This project follows a layered architecture and incorporates essential features like authentication, authorization, logging, and error handling.
---

## Table of Contents
- [Technologies Used](#technologies-used)
- [System Architecture](#system-architecture)
- [Key Features](#key-features)
- [Authentication and Authorization](#authentication-and-authorization)
- [API Endpoints](#api-endpoints)
- [Middleware](#middleware)
- [Business Logic](#business-logic)
- [Data Layer](#data-layer)
- [Validation and Filters](#validation-and-filters)
- [Exception Handling](#exception-handling)
- [Data Protection](#data-protection)
- [Installation and Setup](#installation-and-setup)
- [Usage](#usage)
- [License](#license)

---

## Technologies Used

- **Framework**: ASP.NET Core
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity / JWT
- **Dependency Injection**
- **Middleware for Logging and Maintenance Mode**
- **Data Protection**: ASP.NET Core Data Protection
- **Exception Handling**: Global Exception Middleware

---

## System Architecture

This application is designed with a **three-layered architecture** to separate concerns and improve modularity:

1. **Presentation Layer (API Layer)**: Contains Controllers for handling API requests.
2. **Business Layer (Service Layer)**: Contains the core business logic.
3. **Data Access Layer (Repository Layer)**: Manages database operations, using Entity Framework for data transactions.

---

## Key Features

### Authentication and Authorization
- **JWT** (JSON Web Token): Secures API endpoints and enables user roles and permissions.
- **Roles**: Includes roles like "User" and "Admin" for different access levels.


![Authentication and Authorization](image)

---

## API Endpoints

The system provides comprehensive API endpoints for interacting with hotel data. These include:

- **GET**: Retrieve data (e.g., list of members, user)
- **POST**: Create new records (e.g., member,exercise,user etc)
- **PUT**: Update existing records
- **PATCH**: Partially update records
- **DELETE**: Remove members

![API Endpoints](images/logo.png)

---

## Middleware

### Logging Middleware:
- Logs every incoming request, capturing details such as URL, request time, and customer identity.

### Maintenance Mode Middleware:
- This middleware can activate a maintenance mode for the system. This functionality is supported by an additional database table to control maintenance status.

---

## Business Logic

The **Business Layer** implements various services, with logic encapsulated to ensure data integrity and proper application flow. This layer handles crucial operations such as:
- Room availability checks
- Booking status updates
- Guest registration

---

## Data Layer

- **Entity Framework Core (Code First)**: Defines models and relationships, including one many-to-many relationship (e.g., between Guests and Rooms).
- **Repository Pattern and Unit of Work**: Ensures efficient data operations and separation of data access code.

---

## Validation and Filters

### Model Validation:
Ensures data accuracy with validation rules such as:
- Valid email format for guests.
- Required fields like password and email.

### Action Filters:
A custom Action Filter restricts access to specific APIs based on time periods, ensuring that certain features are accessible only during specified hours.

---

## Exception Handling

A **Global Exception Handling** mechanism is implemented to intercept and manage errors across the application. This feature returns a unified error response to users, improving the system's resilience and user experience.

---

## Data Protection

**Data Protection** safeguards sensitive information such as passwords. It is implemented using **ASP.NET Core Data Protection**, ensuring that passwords and sensitive data are stored securely.

---

## Installation and Setup

### 1. Clone the Repository:
```bash
git clone <repository-url>
