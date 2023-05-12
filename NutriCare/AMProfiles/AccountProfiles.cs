using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NutriCare.DTOs;
using NutriCare.Models;

namespace NutriCare.AMProfiles
{
    public class AccountProfiles : Profile
    {
        public AccountProfiles()
        {
            CreateMap<Account, AccountDTO>();

            CreateMap<Allergy, AllergyDTO>();

            CreateMap<Intolerance, IntoleranceDTO>();
           
            
        }
    }
}
