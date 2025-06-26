# Accounting Ledger System

Migration

Package manager console commands
```
Add-Migration InitialCreate -Project AccountingLedgerSystem.Persistence -StartupProject AccountingLedgerSystem.API
Update-Database -Project AccountingLedgerSystem.Persistence -StartupProject AccountingLedgerSystem.API
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
