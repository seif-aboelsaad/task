using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using route_Tsak.Models;
using route_Tsak.Repo;
using route_Tsak.Repositories;
using route_Tsak.Services;
using System.Text;

namespace route_Tsak
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("OldDefaultConnection");
            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add services to the container. 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle 
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TaskDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(option =>
            option.DefaultAuthenticateScheme = "myschema")
    .AddJwtBearer("myschema", option =>
    {
        string key = "welcome to my account HOMIE TOMY CJ ALEX";
        var secertKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        option.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = secertKey,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    }
                );
            builder.Services.AddScoped<TaskDbContext>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IcostumerServices, CustomerService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<iProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
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


            app.MapControllers();

            app.Run();
        }
    }
}