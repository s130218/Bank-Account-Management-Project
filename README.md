#Bank-Account-Management-Project

- Bank Account Management API
- This project is a comprehensive Bank Account Management Application with a well-organized structure for both frontend and backend         components.
- The frontend is developed using ASP.NET Core MVC, ensuring a robust and responsive user interface, while the backend is built with        ASP.NET Core Web API, handling the core business logic and database operations efficiently.
- The application includes various APIs for authentication, authorization, and transaction management.

- **Features**
- User account creation
- Admin account activation
- Deposit and withdrawal transactions
- Transaction history viewing
- JWT Authentication for secure access

---

**Presentation**

- **Login Page**

![login_img](https://github.com/user-attachments/assets/e33040b3-6e38-4af8-84d4-1dc91bd29ddd)

---

- **Application Users List Page**
![application_users_view_by_admin](https://github.com/user-attachments/assets/26a33359-f84a-41c1-b191-8df098a403a4)

---

- **Create New Account**
![create_new_account_page](https://github.com/user-attachments/assets/5c38ffc5-9fba-4e38-9490-3ad9e268a3d2)

---

- **Total Balance**
![total_balanace_of_each_user](https://github.com/user-attachments/assets/b851fc17-fd51-4f4b-9ee5-745d95f46139)

---

![image](https://github.com/user-attachments/assets/b69d61e7-94a8-4668-a272-de06788716e1)

- **Deposit and withdraw**
  
![deposit_withdraw_page](https://github.com/user-attachments/assets/59ced35d-d809-43da-aac0-d0ca54f6bf69)

---

- **Transaction Statement**
![transaction_statement_for_indivudual_user](https://github.com/user-attachments/assets/61d4f6d7-e690-475f-a9fc-cd59dabbaead)

---

- **Technologies Used**
- ASP.NET Core MVC (Frontend)
- ASP.NET Core Web API (Backend)
- Entity Framework Core
- SQL Server
- JWT for authentication


**Fixed Roles:**

- Customer
- Admin
- SuperAdmin

# API Endpoints

### 1. User Registration
- **API**: `v1/auth/register`
- **Method**: `POST`
- **Request Body**:
  - `email`: string
  - `name`: string
  - `phoneNumber`: string
  - `password`: string
- **Response**:
  - `messageType`: Warning
  - `message`: ["Username 'sunil@gmail.com' is already taken."]
  - `status`: false

---

### 2. User Login
- **API**: `v1/auth/login`
- **Method**: `POST`
- **Request Body**:
  - `username`: string
  - `password`: string
- **Response**:
  - `data`: 
    - `token`: JWT token string
    - `expiryMinutes`: 29
  - `messageType`: Success
  - `message`: ["Login successful"]
  - `status`: true

---

### 3. User List
- **API**: `v1/auth/users`
- **Method**: `GET`
- **Required Role**: Admin
- **Response Body**:
  - `data`: 
    - User 1:
      - `userId`: 1c2db565-d70a-429b-a679-834073d320a6
      - `name`: sadmin
      - `userName`: sadmin
      - `email`: superadmin@example.com
      - `phoneNumber`: null
      - `roles`: ["SuperAdmin"]
    - User 2:
      - `userId`: 4658749e-040f-46d2-bdfa-bad24f5c188f
      - `name`: Sunil
      - `userName`: sunil@gmail.com
      - `email`: sunil@gmail.com
      - `phoneNumber`: 98464573
      - `roles`: ["Customer"]
  - `messageType`: Success
  - `message`: []
  - `status`: true

---

### 4. Delete User
- **API**: `v1/auth/users/{userId}`
- **Method**: `DELETE`
- **Required Role**: SuperAdmin
- **Request Body**: None (userId is passed in URL)

---

### 5. Update User Role
- **API**: `v1/auth/role/assign`
- **Method**: `PUT`
- **Required Role**: SuperAdmin
- **Request Body**:
  - `userId`: string
  - `role`: string
- **Response**:
  - `messageType`: Success
  - `message`: ["User role has been updated successfully."]
  - `status`: true

---
