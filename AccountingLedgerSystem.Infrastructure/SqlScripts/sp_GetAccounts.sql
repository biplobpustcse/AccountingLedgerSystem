IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAccounts')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GetAccounts
    AS
    BEGIN
        SELECT * FROM Accounts;
    END
    ')
END
