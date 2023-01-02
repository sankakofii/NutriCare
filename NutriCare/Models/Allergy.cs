﻿namespace NutriCare.Models
{
    public class Allergy
    {
        public int AllergyId { get; set; }
        public string Type { get; set; } = string.Empty;
        public List<Account>? Accounts { get; set; }
    }
}