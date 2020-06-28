# Contact Information
Web application to maintaining contact information.

# Technology Stack:
Backend: DotnetCre 3.1
Frontend: Angular 8
Database: Microsoft Azure MS-SQL
ORM: EFCore
Hosting Server:  Microsoft Azure Web App

Hosted Url: https://contactinformationweb20200628052334.azurewebsites.net/

# How to use?
You can simply clone this repo from the CLI:
git clone https://github.com/AtulChavan/contactinfo.git

Next, navigate to ClientApp folder under ContactInformtion.Web folder then install the dependencies using:
npm install

You can also Run this project from Visual Studio by selecting ContactInformtion.Web as startup project. If IIS Express doesn't work try with Kestrel web server.

Folder Structure:
# 3 Projects
1. ContactInformtion.Web: Web layer, contains Angular client code, Asp.Net Core web api code.
2. ContactInformtion.Domain: Business layer, contains domain model and handlers.
3. ContactInformtion.Repository: Repository layer, contains repository model, DbContext and DB communication code.
