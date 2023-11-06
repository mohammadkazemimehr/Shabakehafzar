using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shabakehafzar.API.Base.Middleware;
using Shabakehafzar.API.Helper.DependencyInjection;
using Shabakehafzar.API.Helper.Extentions;
using Shabakehafzar.Data.Context;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddDbContext<IAppDataContext, AppDataContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("Main")));

builder.Services.RegisterIdentity();
builder.Services.RegisterAllServices();

// Configure JWT
builder.Services.RegisterAuthentication(builder.Configuration);


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.RegisterSwagger();

var app = builder.Build().Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.UseMiddleware<ExceptionHandllerMiddleware>();

app.MapControllers();

app.Run();
app.Run();
