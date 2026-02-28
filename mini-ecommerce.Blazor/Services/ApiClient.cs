using System.Net.Http.Json;

namespace mini-ecommerce.Blazor.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<T?> Get<T>(string url)
    {
        return await _http.GetFromJsonAsync<T>(url);
    }

    public async Task<TResponse?> Post<TRequest, TResponse>(string url, TRequest body)
    {
        var response = await _http.PostAsJsonAsync(url, body);

        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}