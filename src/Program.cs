using FSWebApi.Authorization;
using FSWebApi.Data;
using FSWebApi.Interfaces;
using FSWebApi.Services;
using FSWebApi.Utils.ErrorHandling;
using FSWebApi.Utils.JsonConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2022-01-01")
    });

    options.MapType<TimeOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "time",
        Example = new OpenApiString("15:14:02.28045")
    });

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityReq);

});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("AppDbConnection"));
});

builder.Services.AddScoped<IJwtUtils, JwtUtils>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarpoolService, CarpoolService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
