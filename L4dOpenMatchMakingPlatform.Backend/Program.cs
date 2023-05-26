using L4dOpenMatchMakingPlatform.Backend.Repositories;
using L4dOpenMatchMakingPlatform.Backend.Services;
using Microsoft.AspNetCore.SignalR;

namespace L4dOpenMatchMakingPlatform.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, BaseModeAuthenticationService>();
            builder.Services.AddSingleton<CustomGameRepository>();
            builder.Services.AddSingleton<CustomGameService>();
            builder.Services.AddSingleton<ServerEndpointRepository>();
            builder.Services.AddSingleton<ServerEndpointService>();
            builder.Services.AddSingleton<BindService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllers();
                    endpoints.UseModeQueueManager();
                }
            );

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
