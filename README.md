Bank Account Management API:
This project is a comprehensive Bank Account Management Application with a well-organized structure for both frontend and backend components. The frontend is developed using ASP.NET Core MVC, ensuring a robust and responsive user interface, while the backend is built with ASP.NET Core Web API, handling the core business logic and database operations efficiently. The application includes various APIs for authentication, authorization, and transaction management.

Features
User account creation
Admin account activation
Deposit and withdrawal transactions
Transaction history viewing
JWT Authentication for secure access


Technologies Used
ASP.NET Core MVC (Frontend)
ASP.NET Core Web API (Backend)
Entity Framework Core
SQL Server
JWT for authentication


Fixed Roles: 
Customer
Admin
SuperAdmin

API Endpoints:
For User Registrartion: 
API: v1/auth/register
Method: POST
Request Body:
{
  "email": "string",
  "name": "string",
  "phoneNumber": "string",
  "password": "string"
}

Response:
{
  "messageType": "Warning",
  "message": [
    "Username 'sunil@gmail.com' is already taken."
  ],
  "status": false
}


For User Login: 
API: v1/auth/login
Method:POST
Request Body:
{
  "username": "string",
  "password": "string"
}


Response
{
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InN1cGVyYWRtaW5AZXhhbXBsZS5jb20iLCJzdWIiOiIxYzJkYjU2NS1kNzBhLTQyOWItYTY3OS04MzQwNzNkMzIwYTYiLCJuYW1lIjoic2FkbWluIiwicm9sZSI6IlN1cGVyQWRtaW4iLCJuYmYiOjE3MjE1Njk3MTMsImV4cCI6MTcyMTU3MTUxMywiaWF0IjoxNzIxNTY5NzEzLCJpc3MiOiJhY2NvdW50LWF1dGgtYXBpIiwiYXVkIjoiYWNjb3VudC1jbGllbnQifQ.8vtGgtWabutgLr3tyPKISI0AxmFDNLVvGzz8i3L9K6I",
    "expiryMinutes": 29
  },
  "messageType": "Success",
  "message": [
    "Login successful"
  ],
  "status": true
}


For User List: 
API: v1/auth/users
Method: GET
Required role: Admin
Response Body:
{
  "data": [
    {
      "userId": "1c2db565-d70a-429b-a679-834073d320a6",
      "name": "sadmin",
      "userName": "sadmin",
      "email": "superadmin@example.com",
      "phoneNumber": null,
      "roles": [
        "SuperAdmin"
      ]
    },
    {
      "userId": "4658749e-040f-46d2-bdfa-bad24f5c188f",
      "name": "Sunil",
      "userName": "sunil@gmail.com",
      "email": "sunil@gmail.com",
      "phoneNumber": "98464573",
      "roles": [
        "Customer"
      ]
    }
  ],
  "messageType": "Success",
  "message": [],
  "status": true
}



To Delete User: 
Method:Delete
API: v1/auth/users/{userId}
Required role: SuperAdmin
Request Body:



To update user role: 
Method: PUT
API: v1/auth/role/assign
Required role: SuoerAdmin
Request Body:
{
  "userId": "string",
  "role": "string"
}

Response: {
  "messageType": "Success",
  "message": [
    "User role has been updated successfully."
  ],
  "status": true
}


