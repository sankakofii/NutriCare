using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NutriCare.Middleware;
using NutriCare.Models;
using NutriCare.Services.AccountService;
using NutriCare.VerificationService;
using System.Text.Json.Serialization;

namespace NutriCare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Key Auth", Version = "v1" });
                    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                    {
                        Description = "ApiKey must appear in header",
                        Type = SecuritySchemeType.ApiKey,
                        Name = "NCApiKey",
                        In = ParameterLocation.Header,
                        Scheme = "ApiKeyScheme"
                    });
                    var key = new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    };
                    var requirement = new OpenApiSecurityRequirement
                        {
                                 { key, new List<string>() }
                        };
                    c.AddSecurityRequirement(requirement);
                });

            //Ignoring cycles
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //CORS policy
            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //phone service
            builder.Services.AddScoped<IPhoneService, PhoneService>();

            //account service
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("corsapp");

            //app.UseMiddleware<ApiKeyMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}