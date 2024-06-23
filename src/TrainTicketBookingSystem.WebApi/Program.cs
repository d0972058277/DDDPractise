using Microsoft.EntityFrameworkCore;
using TrainTicketBookingSystem;
using TrainTicketBookingSystem.WebApi;
using TrainTicketBookingSystem.WebApi.Application.RegisterTrain;
using TrainTicketBookingSystem.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITrainRepository, TrainRepository>();
builder.Services.AddTransient<RegisterTrainCommandHandler>();

builder.Services.AddDbContext<TrainTicketBookingSystemDbContext>(opt =>
{
    var connectionString =
        "Server=localhost; Port=3306; User ID=root; Password=root; Database=TrainTicketBookingSystem;";
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program
{
}