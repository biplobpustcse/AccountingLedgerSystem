IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddJournalEntryLine')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_AddJournalEntryLine
    @JournalEntryId INT,
    @AccountId INT,
    @Debit DECIMAL(18, 2),
    @Credit DECIMAL(18, 2)
	AS
	BEGIN
		INSERT INTO JournalEntryLines (JournalEntryId, AccountId, Debit, Credit)
		VALUES (@JournalEntryId, @AccountId, @Debit, @Credit);
	END	
    ')
END
