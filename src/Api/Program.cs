using System.Reflection;
using challenge_moto_connect.Application.Services;
using challenge_moto_connect.Domain.Interfaces;
using challenge_moto_connect.Infrastructure;
using challenge_moto_connect.Infrastructure.Persistence.Context;
using challenge_moto_connect.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace challenge_moto_connect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = builder.Configuration["Swagger:Title"],
                        Description = builder.Configuration["Swagger:Description"],
                        Contact = new OpenApiContact()
                        {
                            Name = "Moto Connect",
                            Email = "rm558424@fiap.com.br",
                        },
                    }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                x.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHistoryService, HistoryService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Enable Swagger UI in all environments (useful for App Service)
            app.UseSwagger();
            app.UseSwaggerUI();

            // Only redirect to HTTPS during Development to avoid issues on Linux App Service
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet(
                "/",
                context =>
                {
                    context.Response.Redirect("/swagger", permanent: false);
                    return Task.CompletedTask;
                }
            );

            app.Run();
        }
    }
}
