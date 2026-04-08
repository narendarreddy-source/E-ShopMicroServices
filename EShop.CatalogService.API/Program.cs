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
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogDbwindows")));

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console();
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
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            dbContext.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}



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
