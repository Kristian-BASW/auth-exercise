using DataAccess;
using DataAccess.Repositories;
using Security;
using Security.Services;
using Security.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructureServices();
builder.Services.ConfigureDomainServices();
builder.Services.AddControllers();

builder.Services.AddCors(s => s
    .AddPolicy("CorsPolicy", t => t.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}
app.MapControllers();

app.Run();
