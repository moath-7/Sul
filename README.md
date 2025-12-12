# Real Estate Management (ASP.NET Core MVC)

This is a simple Real Estate Management system built with ASP.NET Core MVC and Entity Framework Core (MySQL). It provides basic CRUD operations for the following entities:

- Properties (العقارات)
- Customers (العملاء)
- Agents (الوكلاء)
- Contracts (العقود)
- Payments (المدفوعات)

Note: The application's UI labels (menus, page headings and table column headers) have been localized to Arabic for presentation. These are visual-only changes: controller names, routes, model names, database schema, and code remain unchanged.

## Features

- Full CRUD for Properties, Customers, Agents, Contracts, and Payments
- Select lists for foreign-key relationships in create/edit forms
- Anti-forgery protection on POST actions
- Basic seed data for initial testing

## Requirements

- .NET 10 SDK
- MySQL server (tested with local MySQL/XAMPP)

## Setup

1. Update the connection string in `appsettings.json` (DefaultConnection) to point to your MySQL server.

2. From the project root, restore and build:

```powershell
cd "g:\241\Sul"
dotnet restore
dotnet build
```

3. Run the application:

```powershell
dotnet run
```

The app will typically listen on `http://localhost:5149` (confirm from the console output).

## Notes about UI localization

- The visible text in the navigation bar, the home page, and the entity `Index` table headings and action labels have been updated to Arabic for the UI.
- The underlying code (controller class names, action names, view file names, model property names, and database schema) have NOT been changed. This ensures all routes and server-side logic continue to work the same way.

## Contributing / Next steps

- If you want the UI to support switching languages, consider adding localization resources and wiring ASP.NET Core localization services.
- You may also translate form labels and validation messages if full Arabic localization is desired.

## License

This repository contains example code provided as-is for demonstration and learning purposes.
