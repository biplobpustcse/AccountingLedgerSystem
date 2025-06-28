﻿namespace AccountingLedgerSystem.Application.Helpers;

public static class PasswordHelper
{
    public static string GeneratePasswordHash(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
