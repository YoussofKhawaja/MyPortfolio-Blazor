using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using YoussofPortfolio.API.Interfaces;
using YoussofPortfolio.API.Models;
using YoussofPortfolio.API.Services;
using YoussofPortfolio.API.Database;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton(builder.Configuration.GetSection("AppSettings").Get<AppSettings>());

builder.Services.AddScoped<IProject, ProjectService>();
builder.Services.AddScoped<IMail, MailService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddSerilog((services, lc) => lc
     .ReadFrom.Configuration(builder.Configuration)
     .ReadFrom.Services(services)
     .Enrich.FromLogContext()
     .WriteTo.Console());
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicyProd", policy =>
   {
       policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://youssofkhawaja.com", "https://www.youssofkhawaja.com");
   });
    options.AddPolicy("CorsPolicyDev", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("clientLimit", options =>
    {
        options.PermitLimit = 3;
        options.Window = TimeSpan.FromDays(1); // 1 day window
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5; // Set a reasonable queue limit
    });
    rateLimiterOptions.OnRejected = (context, cancellationToken) =>
    {
        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
        }

        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

        return new ValueTask();
    };
});

var app = builder.Build();

//migrate database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicyDev");
}
else
{
    app.UseCors("CorsPolicyProd");
}

app.UseSerilogRequestLogging();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
