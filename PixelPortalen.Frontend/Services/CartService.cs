using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PixelPortalen.Shared.Models;
using System.Text.Json;

public class CartService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly IJSRuntime _js;

    private string _userId;
    private List<CartItem> _cartItems = new();

    public event Action OnChange;

    public CartService(HttpClient httpClient, AuthenticationStateProvider authProvider, IJSRuntime js)
    {
        _httpClient = httpClient;
        _authProvider = authProvider;
        _js = js;
    }

    public async Task LoadCart()
    {
        await GetUserId();

        if (string.IsNullOrEmpty(_userId))
        {
            var json = await _js.InvokeAsync<string>("cartStorage.loadCart");
            _cartItems = string.IsNullOrWhiteSpace(json)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }
        else
        {

            var json = await _js.InvokeAsync<string>("cartStorage.loadCart");
            var localCart = string.IsNullOrWhiteSpace(json)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();


            _cartItems = await _httpClient.GetFromJsonAsync<List<CartItem>>($"api/cart/{_userId}") ?? new();

            foreach (var item in localCart)
            {
                var existing = _cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (existing != null)
                {
                    existing.Quantity += item.Quantity;
                }
                else
                {
                    item.UserId = _userId;
                    _cartItems.Add(item);
                }

                await _httpClient.PostAsJsonAsync("api/cart", new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserId = _userId
                });
            }

            await _js.InvokeVoidAsync("cartStorage.clearCart");
        }

        OnChange?.Invoke();
    }

    private async Task SaveToLocalStorage()
    {
        var json = JsonSerializer.Serialize(_cartItems);
        await _js.InvokeVoidAsync("cartStorage.saveCart", json);
    }

    private async Task GetUserId()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState?.User;

        _userId = user?.Identity?.IsAuthenticated == true
            ? user.FindFirst("id")?.Value
            : null;
    }

    public async Task AddToCart(Product product)
    {
        var existing = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);

        if (product.Stock != null && existing != null && existing.Quantity >= product.Stock)
            return;

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            _cartItems.Add(new CartItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 1,
                UserId = _userId,
                Price = product.Price
            });
        }

        if (string.IsNullOrEmpty(_userId))
        {
            await SaveToLocalStorage();
        }
        else
        {
            await _httpClient.PostAsJsonAsync("api/cart", new CartItem
            {
                ProductId = product.Id,
                Quantity = 1,
                UserId = _userId,
                Price = product.Price
            });
        }

        OnChange?.Invoke();
    }

    public async Task DecreaseQuantity(Product product)
    {
        var existing = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);
        if (existing == null) return;

        if (existing.Quantity > 1)
        {
            existing.Quantity--;
            if (!string.IsNullOrEmpty(_userId))
            {
                await _httpClient.PostAsJsonAsync("api/cart", new CartItem
                {
                    ProductId = product.Id,
                    Quantity = -1,
                    UserId = _userId
                });
            }
        }
        else
        {
            _cartItems.Remove(existing);
            if (!string.IsNullOrEmpty(_userId))
                await _httpClient.DeleteAsync($"api/cart/{_userId}/{product.Id}");
        }

        if (string.IsNullOrEmpty(_userId))
            await SaveToLocalStorage();

        OnChange?.Invoke();
    }

    public async Task RemoveFromCart(Product product)
    {
        var existing = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);
        if (existing == null) return;

        _cartItems.Remove(existing);

        if (!string.IsNullOrEmpty(_userId))
            await _httpClient.DeleteAsync($"api/cart/{_userId}/{product.Id}");
        else
            await SaveToLocalStorage();

        OnChange?.Invoke();
    }

    public async Task ClearCart()
    {
        _cartItems.Clear();

        if (!string.IsNullOrEmpty(_userId))
            await _httpClient.DeleteAsync($"api/cart/{_userId}");
        else
            await _js.InvokeVoidAsync("cartStorage.clearCart");

        OnChange?.Invoke();
    }

    public List<CartItem> GetCartItems() => _cartItems;
    public int GetCartCount() => _cartItems.Sum(x => x.Quantity);
    public decimal GetTotalPrice() => _cartItems.Sum(x => x.Product.Price * x.Quantity);
}
