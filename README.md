<H1 align="center">OWIN Login and Authentication Structure</H1>
<p align="center">🚀 Project to create a structure using OWIN Authentication for future references</p>

## Resources Used

* OWIN



<div align="center">
  <h3>Login</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/eadef139-4faa-466b-bc60-89ba467d175d" style="width:100%">
</div>


<div align="center">
   <h3>Registration</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/f8c91505-2b2f-4a57-8ca4-06f0cb0cf38c" style="width:100%">
</div>


<div align="center">
  <h3>Registration completed successfully</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/a70603a4-4f62-4f15-a3ed-8c8de43f84e8" style="width:100%">
</div>


<div align="center">
  <h3>Authenticated</h3>
<img src="https://github.com/lucasmargui/ASP_Login_Autenticacao/assets/157809964/f8546b03-49b1-4725-b153-e88e2cd0941d" style="width:100%">
</div>


## Database Create.

Create DBUsuarios Database.

## Application initialization

  <details>
   <summary>Click to show content</summary>
  
Application startup configuration implementing OWIN

```
Startup.cs
```

The antifogery token is a system to identify the user and prevent third-party websites from sending this form, it needs a unique identifier to identify that it is the user who is sending the “form”, so here we are passing what is in the claim as an identifier unique that was defined in the Authentication Controller in the Login action
```
  app.UseCookieAuthentication(new CookieAuthenticationOptions
  {
  AuthenticationType = "ApplicationCookie",
  LoginPath = new PathString("/Autenticacao/Login")
  });

  AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";

```

</details>
 


  ## Controllers

  <details>
   <summary>Click to show content</summary>
  
### AuthenticationController.cs

Responsible for Registration and Login, creating an authentication cookie through the Claim with Name and Login
```
Controllers/AutenticacaoController.cs
```

Creating identity to generate authentication cookie at Login

```
var identity = new ClaimsIdentity(new[]
{
  new Claim(ClaimTypes.Name, user.Name),
  new Claim("Login", user.Login)
}, "ApplicationCookie");

Request.GetOwinContext().Authentication.SignIn(identity);

```

Destroying authentication cookie through Logout

```
Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
```

   ### PanelController.cs

Using authorize, if the person is not authenticated by OWIN, they will be redirected to the path specified in Startup.cs created in the root folder

```
Controllers/PanelController.cs
```

   ### ProfileController.cs

   Controller used to perform actions when the user is authorized, such as implementing password changes.

```
Controllers/PanelController.cs
```

Get the user who is authenticated in Owin

```
var identity = User.Identity as ClaimsIdentity;
```

  
Identity has several claims but in Authentication/login there is a claim with login and name that were passed to create the authentication cookie

```
var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
```

</details>


## Entity Framework execution in IDE's: VS 2015/2017:
<details>
   <summary>Click to show content</summary>
    When executing the commands:
 
   ```
     Enable-Migrations
   ```
   It is
  
   ```
     Update-Database -Verbose
   ```
  
In the most recent versions of Visual Studio (2015/2017), it is necessary to create a new instance of sql localdb on your computer. Which can be created in the following way:

Step 1: Open cmd and execute the following command:
   ```
   SqlLocalDB.exe create "Local"
   ```
Step 2: Run the instance with the following command:
   ```
   SqlLocalDb.exe start
   ```
  
Step 3: Go to the 'Package Manager Console' and execute the following command:
   ```
   Update-Database -Verbose
   ```

## Changing the connection string

```
Web.Config
```
  Change connection string to make connection between Entity framework and database
 
```
   <connectionStrings>
     <add name="Registration"
          connectionString="Data Source= (localdb)\Local;Initial Catalog=DbUsuarios;Integrated Security=True;"
          providerName="System.Data.SqlClient" />
   </connectionStrings>

```


</details>



