using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Web_Labb2.BlazorServer;

public class ApiService(HttpClient httpClient, ProtectedLocalStorage localStorage)
{
    private async Task AddAuthHeaderAsync()
    {
        var token = (await localStorage.GetAsync<string>("authToken")).Value;
        if (!string.IsNullOrEmpty(token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        await AddAuthHeaderAsync();
        return await httpClient.GetFromJsonAsync<T>(url);
    }

    public async Task<TResponse?> PostAsync<TResponse, TRequest>(string url, TRequest data)
    {
        await AddAuthHeaderAsync();
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<TResponse>(json);
    }

    public async Task<TResponse?> PutAsync<TResponse, TRequest>(string url, TRequest data)
    {
        await AddAuthHeaderAsync();
        var response = await httpClient.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return typeof(TResponse) == typeof(string) ? default : JsonConvert.DeserializeObject<TResponse>(json);
    }

    public async Task<bool> DeleteAsync(string url)
    {
        await AddAuthHeaderAsync();
        var response = await httpClient.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    public async Task<string?> PutAsync1(string url)
    {
        await AddAuthHeaderAsync();
        var response = await httpClient.PutAsync(url, null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

}
