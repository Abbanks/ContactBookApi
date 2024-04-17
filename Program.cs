
using ContactPageApi.Data;
using ContactPageApi.Helpers;
using ContactPageApi.Models.Entity;
using ContactPageApi.Services;
using ContactPageApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace ContactPageApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                };
            });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.AddScoped<IPhotoService, PhotoService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Fast api",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedData(userManager, roleManager);
            }

            app.MapControllers();

            app.Run();
        }

        private static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            string[] roleNames = { "Admin", "Regular" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Users
            var adminUser = new AppUser
            {
                UserName = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                PhoneNumber = "1234567890"

            };
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");

            for (int i = 1; i <= 20; i++)
            {
                var regularUser = new AppUser
                {
                    UserName = $"user{i}@example.com",
                    FirstName = GetRandomFirstName(),
                    LastName = GetRandomLastName(),
                    Email = $"user{i}@example.com",
                    PhoneNumber = "1234567890"
                };

                if (await userManager.FindByEmailAsync(regularUser.Email) == null)
                {
                    var result = await userManager.CreateAsync(regularUser, "User123!"); 
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(regularUser, "Regular");
                        // Add claims as needed
                        await userManager.AddClaimAsync(regularUser, new Claim("CanUpdateDetails", "true"));
                        await userManager.AddClaimAsync(regularUser, new Claim("CanGetSingleContactById", "true"));
                        await userManager.AddClaimAsync(regularUser, new Claim("CanGetExistingContactsBySearchTerm", "true"));
                    }
                }

            }

            // Seed Claims
             await userManager.AddClaimAsync(adminUser, new Claim("CanAssignRole", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanAssignClaim", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanGetPaginatedExistingContacts", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanGetSingleContactById", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanDeleteContacts", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanGetExistingContactsBySearchTerm", "true"));
             await userManager.AddClaimAsync(adminUser, new Claim("CanUpdateDetails", "true"));

        }

        private static string GetRandomFirstName()
        {
            string[] firstNames = { "John", "Jane", "Michael", "Emily", "David", "Emma", "James", "Olivia", "William", "Sophia" };
            Random random = new Random();
            return firstNames[random.Next(0, firstNames.Length)];
        }

        private static string GetRandomLastName()
        {
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
            Random random = new Random();
            return lastNames[random.Next(0, lastNames.Length)];
        }


    }
}
