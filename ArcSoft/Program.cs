using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;

namespace ArcSoft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile(Path.Combine(builder.Environment.ContentRootPath, "appsettings.json"));

            // JWT Secret Key
            var jwtKey = builder.Configuration["JWT:SecretKey"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                // Eğer JWT:SecretKey değeri appsettings.json'da bulunmuyorsa, varsayılan bir değer kullanabilirsiniz.
                jwtKey = "DefaultSecretKey";
            }
            var key = Encoding.ASCII.GetBytes(jwtKey);

            // Issuer ve Audience kontrolü
            var issuer = builder.Configuration["JWT:Issuer"];
            var audience = builder.Configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("JWT:Issuer ve JWT:Audience değerleri belirtilmelidir.");
            }

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddSingleton<ILoginService, LoginManager>();
            builder.Services.AddSingleton<ILoginDal, EfLoginMaterialsDal>();
            builder.Services.AddSingleton<IBearerTokenService, BearerTokenService>();

            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArcSoft API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication(); // JWT Authentication ekledik
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
