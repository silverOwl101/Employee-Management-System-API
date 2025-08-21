using Azure.Core;
using Employee_Management_System_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Test.Classes
{
    public static class TestTokenContainer
    {
        private static readonly string FilePath = "auth_tokens.json";
        public static string? AccessToken { get; private set; }
        public static string? RefreshToken { get; private set; }
        
        public static void SaveTokens(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;

            var json = JsonSerializer.Serialize(new { accessToken, refreshToken });
            File.WriteAllText(FilePath, json);
        }
        public static void LoadTokens()
        {
            if (!File.Exists(FilePath)) return;

            var json = File.ReadAllText(FilePath);
            var tokens = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            AccessToken = tokens?["accessToken"];
            RefreshToken = tokens?["refreshToken"];
        }
    }
}
