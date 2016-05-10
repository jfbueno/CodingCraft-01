# CodingCraft 01
### The Candy and Snacks Store System

This is an application made to solve the problem of **The Candy and Snacks Store System** on Coding Craft 1.

### Tecnologies
1. **Frontend project** (CodingCraftWebApp)
   * HTML
   * JavaScript
   * AngularJS

2. **Backend** (CodingCraft1)
   * C#
   * SQL Server


### Running
1. Open the **.sln** file with Visual Studio (*CodingCraft-01/CodingCraft1/CodingCraft1.sln*)
2. Rebuild project to download all nuget packages (make sure [nuget package restore](https://docs.nuget.org/consume/package-restore) is enabled)
3. Open package manager and run `PM> update-database`
4. This will create a database named **`CodingCraftDb`** and insert a admin user named **`Jefh`** - his password is **`123456`**
5. Configure a simple HTTP Server to run the frontend application. (See the steps below to run a simple HTTP server with Python)
   * Go to frontend app directory
   * run `> py -m http.server [server-port]`. Example: `py -m http.server 8080`. If using python <= 2.7 change `http.server` to `SimpleHTTPServer`
  


# TODO
- [ ] **Do not show admin menu items to non admin users**
- [ ] View with all expenses and incomes, grouped by month
- [X] Get user of sale in API Request (currently it's being sent from frontend, urgh)
- [ ] Friendly errors messages
- [ ] Client side validations (money, dates, etc.)
- [ ] ~~Improve user interface~~
- [ ] Allow admin user to add new admins
- [ ] Send email alerts to admins for proximity of puchases payment
- [ ] View 'my purchases' - Show sales of logged user
- [X] Show unauthorized message to non authorized users
- [ ] Notify users when payment reminders is sent
