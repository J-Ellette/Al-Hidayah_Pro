using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AlHidayahPro.Data;
using AlHidayahPro.Data.Data;
using AlHidayahPro.Api.Services;
using AlHidayahPro.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add CORS to allow frontend to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5000", "http://localhost:3000", "https://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Configure database - supports both SQLite (development) and PostgreSQL (production)
var databaseProvider = builder.Configuration["Database:Provider"] ?? "SQLite";
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add pooled DbContext factory (used by AI service)
builder.Services.AddPooledDbContextFactory<IslamicDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString ?? throw new InvalidOperationException("PostgreSQL connection string is required"));
    }
    else
    {
        options.UseSqlite(connectionString ?? "Data Source=alhidayah.db");
    }
});

// Also register as scoped for controllers
builder.Services.AddDbContext<IslamicDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString ?? throw new InvalidOperationException("PostgreSQL connection string is required"));
    }
    else
    {
        options.UseSqlite(connectionString ?? "Data Source=alhidayah.db");
    }
});

// Configure JWT authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSecretKeyHere-ChangeInProduction-MinimumLength32Characters!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "AlHidayahPro";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "AlHidayahProUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Register services
builder.Services.AddScoped<IQuranService, QuranService>();
builder.Services.AddScoped<IHadithService, HadithService>();
builder.Services.AddScoped<IAudioService, AudioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IAiService, AiService>();
builder.Services.AddScoped<ILearningService, LearningService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<ISpacedRepetitionService, SpacedRepetitionService>();
builder.Services.AddScoped<IPrayerService, PrayerService>();
builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();

// Add SignalR for real-time features
builder.Services.AddSignalR();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IslamicDbContext>();
    context.Database.EnsureCreated();
    DbSeeder.SeedData(context);
}

// Configure the HTTP request pipeline
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Map SignalR hub
app.MapHub<StudyHub>("/hubs/study");

app.Run();
