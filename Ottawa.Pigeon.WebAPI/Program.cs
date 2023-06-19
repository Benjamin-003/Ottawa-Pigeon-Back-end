using Ottawa.Pigeon.Infrastructure.Authorization;
using Ottawa.Pigeon.WebAPI.Extensions;

using Sentry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

builder.WebHost.UseSentry();

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.

app.UseSwaggerWithVersioning();

app.UseSentryTracing();

app.UseMiddleware<JwtMiddleware>();

//app.UseHttpsRedirection();
app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
