using Contracts;
using MediatR;
using RentOperations;
using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.ConfigureIISIntegration();
services.ConfigureSqlContext(builder.Configuration);
services.ConfigureRepositoryManager();
services.AddMediatR(typeof(Program).Assembly);
services.AddAutoMapper(typeof(Program).Assembly);
services.AddScoped<IRentManager, RentManager>();


services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
