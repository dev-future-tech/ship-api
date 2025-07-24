using System.Text.Json;
using System.Text.Json.Serialization;

namespace MySecureWebApi.security;

public class KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
{
    public async Task<TokenResponse> LoginAsync(string username, string password)
    {
        var baseUrl = Environment.GetEnvironmentVariable("KEYCLOAK_BASE_URL") ?? configuration["Keycloak:BaseUrl"];

        var tokenEndpoint = $"{configuration["Keycloak:BaseUrl"]}/realms/{configuration["Keycloak:realm"]}/protocol/openid-connect/token";
        var clientId = Environment.GetEnvironmentVariable("KEY_CLOAK_CLIENT_ID")  ?? configuration["Keycloak:ClientId"] ?? "";
        var clientSecret = Environment.GetEnvironmentVariable("KEY_CLOAK_CLIENT_SECRET") ?? configuration["Keycloak:ClientSecret"] ?? "";

        var requestBody = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", clientId},
            { "client_secret", clientSecret},
            { "username", username },
            { "password", password }
        };

        var content = new FormUrlEncodedContent(requestBody);
        var response = await httpClient.PostAsync(tokenEndpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResult = JsonSerializer.Deserialize<TokenResponse>(responseContent);
            return tokenResult!;
        }
        throw new Exception("Failed to authenticate with Keycloak");
    }
}

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}
