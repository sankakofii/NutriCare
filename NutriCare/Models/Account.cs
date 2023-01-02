﻿using System.ComponentModel.DataAnnotations;

namespace NutriCare.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        [MaxLength(96)]
        public byte[] Password { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Allergy>? Allergies { get; set; }
        public List<Intolerance>? Intolerances { get; set; }
    }
}