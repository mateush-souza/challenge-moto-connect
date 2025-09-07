 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using challenge_moto_connect.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;
using challenge_moto_connect.Application.Services;

namespace challenge_moto_connect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = builder.Configuration["Swagger:Description"],
                    Contact = new OpenApiContact() { Name = "Moto Connect", Email = "rm558424@fiap.com.br" }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                x.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHistoryService, HistoryService>();
            builder.Services.AddScoped<IVehicleService, VehicleService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


