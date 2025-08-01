using Microsoft.EntityFrameworkCore;
using TPMTwinAPI.Services;
using TPMTwinAPI.Database;

namespace TPMTwinAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<TPMTwinAPI.Services.SprintCandidateService>();
            builder.Services.AddHostedService<TPMTwinAPI.Services.SprintCandidateBackgroundService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<AdoQueryService>();
            builder.Services.AddDbContext<SprintCandidateDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevClient",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Use CORS before authorization and endpoints
            app.UseCors("AllowAngularDevClient");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
