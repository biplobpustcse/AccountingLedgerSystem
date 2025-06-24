IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTrialBalance')
BEGIN
    EXEC('
    CREATE PROCEDURE sp_GetTrialBalance
	AS
	BEGIN
		SELECT 
			A.Id AS AccountId,
			A.Name,
			A.Type,
			SUM(JL.Debit) AS TotalDebit,
			SUM(JL.Credit) AS TotalCredit,
			SUM(JL.Debit - JL.Credit) AS NetBalance
		FROM Accounts A
		LEFT JOIN JournalEntryLines JL ON A.Id = JL.AccountId
		GROUP BY A.Id, A.Name, A.Type;
	END	
    ')
END
