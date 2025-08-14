using System.Security.Claims;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySecureWebApi.Data;
using MySecureWebApi.Repositories;
using MySecureWebApi.security;
using MySecureWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .WithMethods(["POST", "GET", "PUT", "DELETE", "OPTIONS"])
            .AllowCredentials();
    });
});

// Configure Key Vault
var keyVaultUri = builder.Configuration["Vault:VaultURI"];
Console.WriteLine($"KeyVaultUri: {keyVaultUri}");

if (!string.IsNullOrEmpty(keyVaultUri))
{
    var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
}
else
{
    Console.WriteLine($"Local database: {builder.Configuration.GetConnectionString("Starfleet")}");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Starfleet")));    
}

builder.Services.AddScoped<IShipRepository, ShipRepository>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<IOfficerRepository, OfficerRepository>();
builder.Services.AddScoped<IOfficerService, OfficerService>();
builder.Services.AddScoped<IRankRepository, RankRepository>();
builder.Services.AddScoped<IRankService, RankService>();

if (!builder.Environment.IsDevelopment())
{
    var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = domain;
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:ships", policy => policy.Requirements.Add(new HasScopeRequirement("read:ships", domain)));
        options.AddPolicy("read:ranks", policy => policy.Requirements.Add(new HasScopeRequirement("read:ranks", domain)));
        options.AddPolicy("read:officers", policy => policy.Requirements.Add(new HasScopeRequirement("read:officers", domain)));
    
    });
}

// ADd SignalR WebSockets
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

if (!app.Environment.IsDevelopment())
{
    app.UseAuthentication();
    app.UseAuthorization();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();

}

if(app.Environment.IsDevelopment())
    app.MapControllers().AllowAnonymous();
else
    app.MapControllers();

app.MapHub<ChatHub>("/Chat");

app.Run();
