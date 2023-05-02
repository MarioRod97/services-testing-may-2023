using ProductsApi.Adapters;
using ProductsApi.Demo;

// CreateBuilder adds the "standard" good defaults for EVERYTHING

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 
builder.Services.AddSingleton<ISystemClock, SystemClock>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/demo2", (ISystemClock clock) =>
{
    var currentTime = clock.GetCurrent();
    var response = new DemoResponse
    {
        Message = "Hello from the other side!",
        CreatedAt = currentTime,
        GettingCloseToQuittingTime = currentTime.Hour >= 16
    };
    return Results.Ok(response);
});

app.Run();
