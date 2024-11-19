
using SimpleGrpcService;
using SimpleWebAPI.Controllers;
using SimpleWebAPI.Service;

namespace SimpleWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<DataManager>();
            builder.Services.AddSingleton<TestGrpcService>();
            builder.Services.AddGrpc();

            var app = builder.Build();
            app.UseRouting();
            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();            
            
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.UseGrpcWeb();
            app.MapGrpcService<TestGrpcService>().EnableGrpcWeb();

            app.Run();
        }
    }
}
