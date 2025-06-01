using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace MicroservicioUsuarios.Infrastructure.Services
{

    public class KeycloakAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private string? _accessToken;
    private DateTime _tokenExpiryTime;

    public KeycloakAuthService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    //  Get or refresh admin token
    public async Task<string> GetAdminTokenAsync()
    {
        if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiryTime > DateTime.UtcNow.AddMinutes(-1))
            return _accessToken!;

        var parameters = new Dictionary<string, string>
        {
            { "client_id", _config["Keycloak:AdmClientId"]!},
            { "grant_type", "password" },
            { "username", "admin" },
            { "password", "adminpassword" }
        };
        try
        {
            var tokenUrl =
                $"{_config["Keycloak:BaseUrl"]}/realms/{_config["Keycloak:AdmRealm"]}/protocol/openid-connect/token";
            var response = await _httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(parameters));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenJson = JsonDocument.Parse(content).RootElement;

            _accessToken = tokenJson.GetProperty("access_token").GetString();
            var expiresIn = tokenJson.GetProperty("expires_in").GetInt32();
            _tokenExpiryTime = DateTime.UtcNow.AddSeconds(expiresIn);
        }
        catch(Exception ex)
        {
            throw ex;
        }
        

        return _accessToken!;
    }

    //  Create a new user in Keycloak
    public async Task CreateUserAsync(string email, string name, string lastname, string password)
    {
        var token = await GetAdminTokenAsync();
        Console.WriteLine($"T es {token}");

        var createUserUrl = $"{_config["Keycloak:BaseUrl"]}/admin/realms/{_config["Keycloak:UserRealm"]}/users";

        var userPayload = new
        {
            username = email,
            email = email,
            firstName = name,
            lastName = lastname,
            enabled = true,
            credentials = new[]
            {
                new {
                    type = "password",
                    value = password,
                    temporary = false
                }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, createUserUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(userPayload), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create user in Keycloak: {error}");
        }
    }

        public async Task<string> GetUserTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiryTime > DateTime.UtcNow.AddMinutes(-1))
                return _accessToken!;

            var parameters = new Dictionary<string, string>
        {
            { "client_id", _config["Keycloak:AdmClientId"]!},
            { "grant_type", "password" },
            { "username", "admin" },
            { "password", "adminpassword" }
        };

            var tokenUrl = $"{_config["Keycloak:BaseUrl"]}/realms/{_config["Keycloak:AdmRealm"]}/protocol/openid-connect/token";
            var response = await _httpClient.PostAsync(tokenUrl, new FormUrlEncodedContent(parameters));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenJson = JsonDocument.Parse(content).RootElement;

            _accessToken = tokenJson.GetProperty("access_token").GetString();
            var expiresIn = tokenJson.GetProperty("expires_in").GetInt32();
            _tokenExpiryTime = DateTime.UtcNow.AddSeconds(expiresIn);

            return _accessToken!;
        }

        public async Task CambiarContrasenaAsync( string userId, string nuevaContrasena)
        {
            var accessToken = await GetAdminTokenAsync();
            var url = $"http://localhost:8080/admin/realms/{_config["Keycloak:UserRealm"]}/users/{userId}/reset-password";

            var payload = new
            {
                type = "password",
                value = nuevaContrasena,
                temporary = false
            };

            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Contraseña cambiada correctamente.");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al cambiar la contraseña: {response.StatusCode}\n{error}");
            }
        }

        public async Task EnviarCorreoVerificacionAsync(string userId, string redirectUri = null)
        {
            var baseUrl = "http://localhost:8080";
            var adminAccessToken = await GetAdminTokenAsync();


            string url = $"{baseUrl}/admin/realms/{_config["Keycloak:UserRealm"]}/users/{userId}/send-verify-email";

            if (!string.IsNullOrEmpty(redirectUri))
            {
                url += $"?redirect_uri={Uri.EscapeDataString(redirectUri)}";
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminAccessToken);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(" Correo de verificación enviado correctamente.");
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($" Error al enviar el correo de verificación: {response.StatusCode}\n{error}");
            }
        }

        public async Task<bool> ActualizarUsuarioEnKeycloakAsync(string userId, string nuevoNombre, string nuevoApellido, string nuevoCorreo)
        {
            using var httpClient = new HttpClient();

            var tokenAdmin = await GetAdminTokenAsync();

            var baseUrl = "http://localhost:8080";
            string url = $"{baseUrl}/admin/realms/{_config["Keycloak:UserRealm"]}/users/{userId}/";

            var body = new
            {
                firstName = nuevoNombre,
                lastName = nuevoApellido,
                email = nuevoCorreo,
                username = nuevoCorreo
            };

            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenAdmin);

            var response = await httpClient.PutAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}