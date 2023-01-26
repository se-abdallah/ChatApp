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
<<<<<<< HEAD
// builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();
=======
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
builder.Services.AddDbContextPool<DataContext>(options =>
 {
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
 });
<<<<<<< HEAD
// builder.Services.AddControllers().AddJsonOptions(options =>
//  {
// options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//  });
=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6

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
if (app.Environment.IsDevelopment())
{
 app.UseDeveloperExceptionPage();
 //  app.UseSwagger();
 //  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();


//app.UseHttpsRedirection();

<<<<<<< HEAD
app.UseCors(builder => builder.AllowAnyHeader()
=======
app.UseCors(x => x.AllowAnyHeader()
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

<<<<<<< HEAD
// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
=======
// if(args.Length ==1 && args[0].ToLower() == "seeddata") SeedUsers(app);
// void Seed(IHost app)
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
 var context = services.GetRequiredService<DataContext>();
<<<<<<< HEAD
 await context.Database.MigrateAsync();
 await Seed.SeedUsers(context);
}
catch (Exception ex)
{
 var logger = services.GetService<ILogger<Program>>();
=======
 // var userManager = services.GetRequiredService<UserManager<AppUser>>();
 // var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
 await context.Database.MigrateAsync();
 await Seed.SeedUsers(context);
 // await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
 var logger = services.GetRequiredService<ILogger<Program>>();
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
 logger.LogError(ex, "An error occurred during migration");
}
await app.RunAsync();

<<<<<<< HEAD

=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
app.Run();
