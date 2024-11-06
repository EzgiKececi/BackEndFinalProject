using FitGoalsApp.Business.DataProtection;
using FitGoalsApp.Business.Operation.Exercise;
using FitGoalsApp.Business.Operation.Member;
using FitGoalsApp.Business.Operation.Nutrition;
using FitGoalsApp.Business.Operation.Setting;
using FitGoalsApp.Business.Operation.User;
using FitGoalsApp.Data.Context;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using FitGoalsApp.WebApi.Filters;
using FitGoalsApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using static FitGoalsApp.WebApi.Middlewares.GlobalExceptionMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer Token on Textbox below.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });


});

var cs = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<FitGoalsAppDbContext>(options => options.UseSqlServer(cs));


builder.Services.AddScoped<IDataProtection, DataProtection>();

var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
    .SetApplicationName("FitGoalsApp")
    .PersistKeysToFileSystem(keysDirectory);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateLifetime = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))


        };
    });



builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserService,UserManager>();
builder.Services.AddScoped<IExerciseService,ExerciseManager>();
builder.Services.AddScoped<INutritionService, NutritionManager>();
builder.Services.AddScoped<IMemberService,MemberManager>();
builder.Services.AddScoped<ISettingService,SettingManager>();   


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMaintenenceMode();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseLogging();
app.MapControllers();

app.Run();
