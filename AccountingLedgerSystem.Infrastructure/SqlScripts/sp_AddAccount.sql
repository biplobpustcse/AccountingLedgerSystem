IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddAccount')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_AddAccount
    @Name NVARCHAR(100),
    @Type NVARCHAR(50)
	AS
	BEGIN
		INSERT INTO Accounts (Name, Type)
		VALUES (@Name, @Type);
	END	
    ')
END
