# Management System
This project is meant to be used as an example for educational purposes. It is not intended to be used otherwise.

A system that aims to facilitate the file management of an organization.

Requirements:
- .NET support
- IDE to build the executable (needs to be capable of opening .sln projects)
- OS: Windows
- folder structure should remain unchanged 
- a local database, configured in management_system/SYSTEM/SETTINGS/XML_databases.xml (connection string) and with the following mandatory tables:
 1. Users (log in information)
 2. Notifications (notifications; will be automatically generated if missing)
 3. GrupIndex (information about groups)
  
Main programs used in development:
- Micrososft Visual Studio 2022 Community Edition
- Microsoft SQL Server Management Studio 18
- Notepad++

Languages used:
- C# (+Windows Forms)
- SQL 
