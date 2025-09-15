using E_commerceAPI.BLL.Services.Classes;
using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Reposetories.Classes;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using E_commerceAPI.DAL.Utils;
using KAStore.DAL.Data;
using Scalar;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using E_commerceAPI.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using E_commerceAPI.Utils;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace E_commerceAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<ICategoyRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            // Services
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICheckoutService, CheckoutService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmailSender, EmailSetting>();

            // Authentication & JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTOptions")["SecretKey"])),
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.NameIdentifier

                };
              
            });

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(opt => opt.RouteTemplate = "openapi/{documentName}.json");

                app.MapScalarApiReference(opt =>
                {
                    opt.Title = "E_commerceAPI";
                    opt.Theme = ScalarTheme.BluePlanet;
                    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
                });
            }

            // Seed data
            var scope = app.Services.CreateScope();
            var ObjectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await ObjectOfSeedData.DataSeeding();
            await ObjectOfSeedData.IdentityDataSeddingAsyn();

            // Middleware
            app.UseHttpsRedirection();

            // **Important**: Authentication must come before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
