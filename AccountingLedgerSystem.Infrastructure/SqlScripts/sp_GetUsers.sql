IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUsers')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GetUsers
	AS
    BEGIN
        SELECT * FROM dbo.Users;
    END	
    ')
END
