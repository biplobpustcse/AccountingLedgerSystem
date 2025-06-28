# Accounting Ledger System

# 🧾 Modular Accounting Ledger System

A lightweight and secure Accounting Ledger backend built with **ASP.NET Core Web API (.NET 8)**, **Entity Framework Core**, and **SQL Server Stored Procedures**, following **Clean Architecture** principles.

---

## 🚀 Features

- ✅ Clean Architecture (Domain, Application, Infrastructure, API)
- ✅ EF Core with SQL Server (no LINQ – stored procedure based)
- ✅ Account management (`POST`/`GET`)
- ✅ Double-entry journal system (`POST /journalentries`)
- ✅ Trial balance (`GET /trialbalance`)
- ✅ JWT Authentication with access + refresh tokens
- ✅ Swagger support with bearer token input
- ✅ MediatR, AutoMapper, FluentValidation integrated
- ✅ Secure endpoints via `[Authorize]` attribute

---

## 🏗️ Tech Stack

| Layer        | Technology                               |
|--------------|-------------------------------------------|
| Backend API  | ASP.NET Core Web API (.NET 8)            |
| Auth         | JWT (Access + Refresh tokens)            |
| DB Access    | Entity Framework Core (Stored Procedures)|
| DB Engine    | SQL Server                               |
| Architecture | Clean Architecture (Modular)             |
| Patterns     | Repository, CQRS (MediatR)                |

---

## 📁 Project Structure

```
AccountingLedgerSystem/
├── API/ # Entry point - Controllers, Swagger, Auth
├── Application/ # DTOs, Commands, Queries, Handlers, Interfaces
├── Domain/ # Entities (Account, JournalEntry, User)
├── Infrastructure/ # SP repo, services, token service
├── Persistence/ # DbContext, migration config
└── SqlScripts/ # Stored procedure scripts
```


---

## 🔐 Authentication

- Register: `POST /api/users/registration`
- Login: `POST /api/users/login`  
  → returns `accessToken` & `refreshToken`
- Refresh: `POST /api/users/refresh`

Use `accessToken` as a **Bearer token** in Swagger to authorize endpoints.

---

## 🛠️ Setup Instructions

1. **Clone this repository**

```bash
git clone https://github.com/your-username/accounting-ledger-system.git
cd accounting-ledger-system
```
**2. Configure appsettings.json**
```
"Jwt": {
  "SecretKey": "YourVerySecretKeyHere",
  "Issuer": "YourAppName",
  "Audience": "YourAppAudience",
  "DurationInMinutes": "60",
  "RefreshTokenDays": "7"
},
"ConnectionStrings": {
  "DatabaseConnection": "Your_SQL_Server_Connection_String"
}
```


**3. Run EF Migrations**

Package manager console commands
```
Add-Migration InitialCreate -Project AccountingLedgerSystem.Persistence -StartupProject AccountingLedgerSystem.API
Update-Database -Project AccountingLedgerSystem.Persistence -StartupProject AccountingLedgerSystem.API
```
**4. Run the application**
```
dotnet run --project API
```
**5. ✅ Stored procedures will automatically initialize on app startup.**

**🔍 API Endpoints Overview**
| Endpoint                  | Method | Auth Required | Description                 |
| ------------------------- | ------ | ------------- | --------------------------- |
| `/api/accounts`           | GET    | ✅             | Get all accounts            |
| `/api/accounts`           | POST   | ✅             | Create a new account        |
| `/api/journalentries`     | GET    | ✅             | Get all journal entries     |
| `/api/journalentries`     | POST   | ✅             | Create new journal entry    |
| `/api/trialbalance`       | GET    | ✅             | Get trial balance summary   |
| `/api/users/registration` | POST   | ❌             | Register new user           |
| `/api/users/login`        | POST   | ❌             | Authenticate and get tokens |
| `/api/users/refresh`      | POST   | ❌             | Refresh JWT access token    |

**6. Implemented seeding sample data on first application run**

- Seed a few sample Accounts
- Seed a few sample JournalEntries (with lines)
- Run this only once on startup (if DB is empty)
- Use stored procedures only — no EF LINQ inserts

**Seed data for default user and password**
```
email = "admin@example.com"
password = "admin123"
```


## Samle Data for APIs

/api/Accounts
```
{
  "name": "Cash",
  "type": "Asset"
}
```
```
{
  "name": "Office Equipment",
  "type": "Asset"
}
```
```
{
  "name": "Salaries Expense",
  "type": "Expense"
}
```

/api/JournalEntries
```
{
  "date": "2025-06-26T06:44:06.771Z",
  "description": "Paid utility bill",
  "lines": [
    {
      "accountId": 3,  
      "debit": 50000,
      "credit": 0
    },
    {
      "accountId": 1, 
      "debit": 0,
      "credit": 50000
    }
  ]
}
```
