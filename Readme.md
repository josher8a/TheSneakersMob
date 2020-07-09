# The Sneakers Mob Application

Backend repository for the MarketPlace TheSneakersMob

## Prerequisistes
The only prerequisite for running the app is having .Net Core 3.1 SDK installed https://dotnet.microsoft.com/download/dotnet-core/3.1

## Getting Started
1. Clone the repository
2. Change directory to the Api folder
     ```
     cd TheSneakersMob
     ```
3. Restore required packages by running:
      ```
     dotnet restore
     ```
4. Run migrations. This will create a local database named TheSneakersMobDb
    ```
	   dotnet ef database update
    ```
5. Run the api
    ```
     dotnet run
     ``` 
6. Launch [https://localhost:5001](http://localhost:5000) in your browser to test the Api with Swagger

 ### TestUsers
 When the Application is started in development mode the following users are seeded into to database  
 * bob@test.com
 * alice@test.com

  Both users have the same password(Password123.)

  ### Authentication and Authorization
  The application uses IdentityServer(https://identityserver.io/) as the indentity provider. This is a library that implements the OpenId Connect and Oauth2 specifications.  
  You need to register the new application in the file Extensions/IdentityServer/Config.cs with the desired flow (Authorization Code + PKCE is the recommended one for SPAs)
