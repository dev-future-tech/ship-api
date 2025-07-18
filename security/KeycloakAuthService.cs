using System.Text.Json.Serialization;
using System.Text.Json;

namespace Ships.Security
{
    public class KeycloakAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<TokenResponse> LoginAsync(string username, string password)
        {
            var baseUrl = Environment.GetEnvironmentVariable("KEYCLOAK_BASE_URL") ?? _configuration["Keycloak:BaseUrl"];

            var tokenEndpoint = $"{_configuration["Keycloak:BaseUrl"]}/realms/{_configuration["Keycloak:realm"]}/protocol/openid-connect/token";
            var clientId = Environment.GetEnvironmentVariable("KEY_CLOAK_CLIENT_ID")  ?? _configuration["Keycloak:ClientId"] ?? "";
            var clientSecret = Environment.GetEnvironmentVariable("KEY_CLOAK_CLIENT_SECRET") ?? _configuration["Keycloak:ClientSecret"] ?? "";

            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", clientId},
                { "client_secret", clientSecret},
                { "username", username },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(requestBody);
            var response = await _httpClient.PostAsync(tokenEndpoint, content);

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
}