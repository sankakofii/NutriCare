using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NutriCare.Middleware;
using NutriCare.Models;
using NutriCare.Services.AccountService;
using NutriCare.VerificationService;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
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

                    //jwt for swagger
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                });

            //Ignoring cycles
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //JWT Authentication Service
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration.GetSection("JWT:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

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

            app.UseAuthentication();

            app.UseMiddleware<ApiKeyMiddleware>();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}