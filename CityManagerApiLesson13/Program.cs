using CityManagerApiLesson13.Data;
using CityManagerApiLesson13.Data.Abstract;
using CityManagerApiLesson13.Data.Abstractl;
using CityManagerApiLesson13.Data.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container : 
builder.Services.AddControllers().AddJsonOptions
(
    options => 
    { 
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }
);

// adding cors policy : 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",builder =>
    {
        builder.WithOrigins("http://localhost:5173") // react development server url here 
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding repository injections : 
builder.Services.AddScoped<IAppRepository,AppRepository>();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();

// adding injection for database :
var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDataContext>(opt =>
{
    opt.UseSqlServer(conn);
});

// adding auto mapper to the project : 
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// getting random key from 'appsettings.json' which was created by user for encrypting the jwt token : 
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token")?.Value);

// adding authentication for jwt :
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // no groupping with jwt 
            ValidateAudience = false,
        };
    });

// building app and starting the middlewares and services :
var app = builder.Build();

// using CORS : 
app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
