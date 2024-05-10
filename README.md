## ContactBookAPI

## Project Overview
This Contact Book API is designed to manage contact information using the ASP.NET Core Framework. It includes features such as an Identity membership system, pagination, JWT authentication, data storage using SQL-Server database, and picture upload using Cloudinary. The API provides endpoints for CRUD operations on user records and supports two user roles: Admin and Regular

## Project Layout

### API Endpoints

**GET: http://localhost:[port]/User/all?page=[current number]**

-   **Description**: Retrieves a paginated list of all contacts.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin role required.
-   **Pagination**: The list is limited to 50 records per page.

**GET: http://localhost:[port]/User/[id]**

-   **Description**: Retrieves a single contact by ID.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

**GET: http://localhost:[port]/Search?term=[search-term]**

-   **Description**: Retrieves a paginated list of contacts based on a search term.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.
-   **Pagination**: The list is limited to 50 records per page.

**POST: http://localhost:[port]/User/add**

-   **Description**: Creates a new user.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Regular role required.

**PUT: http://localhost:[port]/User/update/[id]**

-   **Description**: Updates a user's details.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

**DELETE: http://localhost:[port]/User/delete/[id]**

-   **Description**: Deletes a user.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin role required.

**PATCH: http://localhost:[port]/User/photo/[id]**

-   **Description**: Updates a user's profile picture.
-   **Authentication**: JWT authentication required.
-   **Authorization**: Admin or Regular role required.

### User Roles

**Admin**

-   **Description**: Has full access to all API endpoints.
-   **Permissions**: Can get paginated records of existing contacts, get a single record of existing contacts either by ID, get paginated records of existing contacts using a search term, delete contacts, and update own record.

**Regular**

-   **Description**: Has limited access to API endpoints.
-   **Permissions**: Can register, update their details, get a single record of existing contacts either by ID, and get paginated records of existing contacts using a search term.

## Libraries and Framework used
The project leverages the following libraries and framework:
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- Cloudinary
- ASP.NET Authentication
- ASP.NET Identity

## Demo
 
![Demo](ContactApi.gif)
 
