using AuthWasmRenderMode.Client.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AuthWasmRenderMode.Client.Services;
public class HttpService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : IHttpService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    public async Task<bool> PostRequest(string url, string data)
    {
        var user = await ((PersistentAuthenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
        var userClaims = user.User.Claims.ToList();
        var tokenClaim = userClaims.FirstOrDefault(cl => cl.Type == "token")?.Value;
   
       
        var token = await _localStorageService.GetItemAsync<string>("token");
        Console.WriteLine(token);
       //_httpClient.BaseAddress = new Uri(url);
        return await Task.FromResult(true);
    }
}

public interface IHttpService
{
    Task<bool> PostRequest(string url, string data);
}
