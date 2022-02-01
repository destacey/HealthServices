# HealthServices
## Description
This web application is an example of how to wire up a single entity from the UI to the database. Current features:
* Ability to view a list of Hospitals
* Ability to create a Hospital
* Ability to edit an existing Hospital
* Ability to delete an existing Hospital

## Dependencies
* .Net 6.0
* LocalDb or SQL Server
* Visual Studio 2022

The startup process will automatically create the database and seed hospital data as long the ASPNETCORE_ENVIRONMENT is set to development.  The connection string is set to use LocalDb by default.  Update the connection string in appsettings.development.json to switch to SQL Server.
