using System.Text;
using API.Data;
using API.Entity;
using API.Helpers;
using API.Interfaces;
using API.Middleware;
using API.Services;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITokenService, TokenService>();
// builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<LogUserActivity>();
//! replace repositories with unit of work repository
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSignalR();
builder.Services.AddSingleton<PresenceTracker>();

builder.Services.AddDbContextPool<DataContext>(options =>
 {
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
 });
// builder.Services.AddControllers().AddJsonOptions(options =>
//  {
// options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//  });

// builder.Services.AddTransient<Seed>();
// builder.Services.AddTransient<DataContext>();

builder.Services.AddIdentityCore<AppUser>(opt =>
{
 opt.Password.RequireNonAlphanumeric = false;
})
.AddRoles<AppRole>()
.AddRoleManager<RoleManager<AppRole>>()
.AddEntityFrameworkStores<DataContext>();

//!Authentication comes before Authorization 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
 options.TokenValidationParameters = new TokenValidationParameters
 {
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
  ValidateIssuer = false,
  ValidateAudience = false,
 };
 options.Events = new JwtBearerEvents
 {
  OnMessageReceived = context =>
  {
   var accessToken = context.Request.Query["access_token"];
   var path = context.HttpContext.Request.Path;
   if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
   {
    context.Token = accessToken;
   }

   return Task.CompletedTask;
  }
 };
});

//!Authorization comes after Authentication   
builder.Services.AddAuthorization(opt =>
{
 opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
 opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin,Moderator"));
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

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");
app.MapFallbackToController("Index", "FallBack");

// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
 var context = services.GetRequiredService<DataContext>();
 var userManager = services.GetRequiredService<UserManager<AppUser>>();
 var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
 await context.Database.MigrateAsync();
 await Seed.ClearConnections(context);
 await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
 var logger = services.GetService<ILogger<Program>>();
 logger.LogError(ex, "An error occurred during migration");
}
await app.RunAsync();

app.Run();
