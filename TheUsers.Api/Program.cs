
using Microsoft.Extensions.Logging;
using TheUsers.Api.Extensions;
using TheUsers.Data.Repositories;
using TheUsers.Domain.Repositories;
using TheUsers.Domain.Services;
using TheUsers.Services;

namespace TheUsers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            //builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddSingleton(typeof(ILogger), ILogger);
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "TheUsers.Api", Version = "v1" });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            app.ConfigureExceptionHandler(app.Logger);    // option 1 to handle exceptions (extension).
            //app.ConfigureCustomExceptionMiddleware();   // option 2 to handle exceptions (middleware).

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
