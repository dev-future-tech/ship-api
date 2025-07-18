using Microsoft.EntityFrameworkCore;
using Ships.Data;
using Ships.Repositories;
using Ships.Security;
using Ships.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");
var realm = builder.Configuration["Keycloak:Realm"];
var authBaseUrl = builder.Configuration["Keycloak:BaseUrl"];



if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));    
}

builder.Services.AddScoped<IShipRepository, ShipRepository>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IOfficerRepository, OfficerRepository>();
builder.Services.AddScoped<IOfficerService, OfficerService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"{authBaseUrl}/realms/{realm}",

        ValidateAudience = true,
        ValidAudience = "account",

        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,

        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
        {
            var client = new HttpClient();
            var keyUri = $"{parameters.ValidIssuer}/protocol/openid-connect/certs";
            var response = client.GetAsync(keyUri).Result;
            var keys = new JsonWebKeySet(response.Content.ReadAsStringAsync().Result);

            return keys.GetSigningKeys();
        }
    };

    options.RequireHttpsMetadata = false; // Only in develop environment
    options.SaveToken = true;
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<KeycloakAuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();

}
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
