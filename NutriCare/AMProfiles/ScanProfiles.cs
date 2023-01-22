using AutoMapper;
using NutriCare.DTOs;
using NutriCare.Models;

namespace NutriCare.AMProfiles
{
    public class ScanProfiles : Profile
    {
        public ScanProfiles()
        {
            CreateMap<CreateScanHistoryDTO, Scan>();


            CreateMap<ResponseDTO, Product>();

            CreateMap<Scan, ScanDTO>()
                .ForMember(
                    dst => dst.code,
                    opt => opt.MapFrom(src => src.Product.Barcode));

            CreateMap<Product, ProductDTO>()
                .ForMember(
                    dst => dst.product_name,
                    opt => opt.MapFrom(src => src.ProductName)
                )
                .ForMember(
                    dst => dst.image_front_url,
                    opt => opt.MapFrom(src => src.ImageFrontUrl)
                )
                .ForMember(
                    dst => dst.image_nutrition_url,
                    opt => opt.MapFrom(src => src.ImageNutritionUrl)
                )
                .ForMember(
                    dst => dst._id,
                    opt => opt.MapFrom(src => src.Barcode)
                )
                .ForMember(
                    dst => dst.allergens,
                    opt => opt.MapFrom(src => src.Allergens)
                )
                .ForMember(
                    dst => dst.allergens_from_ingredients,
                    opt => opt.MapFrom(src => src.AllergensFromIngredients)
                )
                .ForMember(
                    dst => dst.ingredients_text,
                    opt => opt.MapFrom(src => src.IngredientsText)
                )
                ;

        }
    }
}
