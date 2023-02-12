using System.Text;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<LogUserActivity>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddDbContextPool<DataContext>(options =>
 {
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
 });
// builder.Services.AddControllers().AddJsonOptions(options =>
//  {
// options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//  });

// builder.Services.AddTransient<Seed>();
// builder.Services.AddTransient<DataContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
 options.TokenValidationParameters = new TokenValidationParameters
 {
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
  ValidateIssuer = false,
  ValidateAudience = false,
 };
});

builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//  app.UseDeveloperExceptionPage();
//  app.UseSwagger();
//  app.UseSwaggerUI();
// }

app.UseMiddleware<ExceptionMiddleware>();


//app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
 var context = services.GetRequiredService<DataContext>();
 await context.Database.MigrateAsync();
 await Seed.SeedUsers(context);
}
catch (Exception ex)
{
 var logger = services.GetService<ILogger<Program>>();
 logger.LogError(ex, "An error occurred during migration");
}
await app.RunAsync();

app.Run();
