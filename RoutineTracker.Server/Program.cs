using Microsoft.EntityFrameworkCore;
using RoutineTracker.Server.Database;
using RoutineTracker.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<ApplicationContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpoints();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.Migrate();
}


app.MapEndpoints();

app.Run();