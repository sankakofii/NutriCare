﻿namespace NutriCare.Models
{
    public class Intolerance
    {
        public int IntoleranceId { get; set; }
        public string Type { get; set; } = string.Empty;
        public List<Account>? Accounts { get; set; }
    }
}