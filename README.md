##  JWT Authentication API in ASP.NET Core
This project implements JWT-based authentication using ASP.NET Core, Identity, PostgreSQL, and cookie-based token storage. It provides a secure authentication system with password policies, lockout strategies, and token validation, making it ideal for modern web applications or APIs.

# Technologies 
+ ASP.NET Core
+ Entity Framework Core with PostgreSQL
+ ASP.NET Core Identity
+ JWT Bearer Authentication
+ Cookie-based JWT storage

# Authentication Configuration
The application uses JwtBearerDefaults.AuthenticationScheme as the default authentication and challenge scheme.
*Key settings:*
- **Token storage**: is handled via cookies (jwt cookie).
- **Password policy requires**: Uppercase, lowercase, digits, Minimum length: 8 characters.
- **Lockout policy**: 5 failed attempts, 5 minutes lockout duration.

# Next Steps 

+ Add refresh tokens
+ Email confirmation + role management
+ Integrate frontend (React, Angular, etc.)