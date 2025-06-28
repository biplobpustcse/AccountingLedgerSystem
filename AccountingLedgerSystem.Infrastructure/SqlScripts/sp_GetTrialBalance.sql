IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTrialBalance')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GetTrialBalance
	AS
	BEGIN
		SELECT 
			A.Id AS AccountId,
			A.Name AS AccountName,
			A.Type AS AccountType,
			ISNULL(SUM(JL.Debit), 0) AS TotalDebit,
			ISNULL(SUM(JL.Credit), 0) AS TotalCredit,
			ISNULL(SUM(JL.Debit - JL.Credit), 0) AS NetBalance
		FROM Accounts A
		LEFT JOIN JournalEntryLines JL ON A.Id = JL.AccountId
		GROUP BY A.Id, A.Name, A.Type;
	END	
    ')
END
