# Asp.Net Boilerplate sample
This is a simple sample to work with Asp.Net Boilerplate, using Angular as frontend.

<b>Note:</b> 
- All supported features in web app are disabled (user, tenant, role management, localization...), we just focus to the simple Store management: store CRUD, category CRUD and product CRUD.
- The database relationship is made as simple as possible in order to make a small demo.

<b>Database class diagram:</b>
<br/><img src="Documents/Database/Class Diagram.jpg">

<b>Steps to run sample</b>
1. Backend
- Correct the databae connection string in <b>appsettings.json</b> file
- Create a new database in SQL Server, named <b>StoreCMS</b> (you can change the name in <b>appsettings.json</b> file)
- Run <b>Update-Database</b> migration (select <b>EntityFrameworkCore</b> as the default project in Package Manager Console)
- Run the backend
2. Front end
- Update dependencies: <b>npm install</b>
- Run the web app: <b>ng serve</b>
- The default account to login
  - username: <b>admin</b>
  - password: <b>welcome</b>
