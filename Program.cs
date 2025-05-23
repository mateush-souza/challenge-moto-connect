using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using challenge_moto_connect.Controllers;
using challenge_moto_connect.Infrastructure.Persistence.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;
using challenge_moto_connect.Infrastructure.Context;
using challenge_moto_connect.Domain.Entity;

namespace challenge_moto_connect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container depency injector.

            ////Criado uma vez e usamos toda vez que precisamos
            //builder.Services.AddSingleton();

            ////Tenho pre definido e termino de criar quando precisar
            //builder.Services.AddScoped();

            ////Tenho pre definido e pre criado quando precisar termino
            //builder.Services.AddTransient();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration["Swagger:Title"],
                    Description = "API CP-02",
                    Contact = new OpenApiContact() { Name = "Moto Connect", Email = "rm558424@fiap.com.br" }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; 
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //incluir os comentarios no SWAGGER
                x.IncludeXmlComments(xmlPath);


            });

            builder.Services.AddDbContext<ChallengeMotoConnectContext>(options =>
            {
                options.UseOracle(builder.Configuration.GetConnectionString("Oracle"));
            });

            builder.Services.AddScoped<IRepository<Vehicle>, Repository<Vehicle>>();
            /*ADICIONAR DEMAIS CLASSES*/
            /*builder.Services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();*/


            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
