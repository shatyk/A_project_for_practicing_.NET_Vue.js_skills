using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Database;
using System.Text;
using Backend.Models;
using Backend.Interfaces.Jwt;
using Backend.Interfaces;
using Backend.Services.Jwt;
using Backend.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<AppDbContext>();

builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IRefreshTokenValidator, RefreshTokenValidator>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();

JwtSettings jwtSettings = new JwtSettings();
RegisterInviteSecret registerInviteSecret = new RegisterInviteSecret();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Configuration.Bind(nameof(RegisterInviteSecret), registerInviteSecret);
builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton(registerInviteSecret);

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenSecret)),
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });
//builder.Services.AddAuthorization();

builder.Services.AddScoped<ILanguageCacheService, LanguageCacheService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IFundraisingService, FundraisingService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();