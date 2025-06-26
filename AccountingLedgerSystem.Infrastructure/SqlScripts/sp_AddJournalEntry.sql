IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddJournalEntry')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_AddJournalEntry
    @Date DATETIME,
    @Description NVARCHAR(255),
	@NewJournalEntryId INT OUTPUT
	AS
	BEGIN
		INSERT INTO JournalEntries (Date, Description)
		VALUES (@Date, @Description);

		SET @NewJournalEntryId = SCOPE_IDENTITY();
	END	
    ')
END
