using Eshop.Shared;
using Eshop.Shared.Exceptions;
using EShop.CatalogService.Application;
using EShop.CatalogService.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationDI();
builder.Services.AddInfraDI();

builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDb")));

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // your frontend URLs
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    
    try
    {
        Console.WriteLine("Applying migrations...");
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            dbContext.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to apply migrations...");
        Console.WriteLine(ex);
    }
}

app.UseCors("AllowFrontend");

app.UseSerilogRequestLogging();

app.UseMiddleware<CommonResponseMiddleware>();

//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
//        context.Response.ContentType = "application/json";

//        if (exception is NotFoundException)
//        {
//            context.Response.StatusCode = StatusCodes.Status404NotFound;
//        }
//        else
//        {
//            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        }
//        var traceId = context.TraceIdentifier;
//        var response = ApiResponse<object>.FailureResponse(
//            "An unexpected error occurred.",
//            traceId
//        );

//        var json = JsonSerializer.Serialize(response);
//        await context.Response.WriteAsync(json);
//    });
//});


app.UseAuthorization();

app.MapControllers();

app.Run();
