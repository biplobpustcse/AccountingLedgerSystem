IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddUsers')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_AddUsers
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255)
	AS
	BEGIN
		SET NOCOUNT ON;

		INSERT INTO Users (Email, PasswordHash)
		VALUES (@Email, @PasswordHash);
	END	
    ')
END
