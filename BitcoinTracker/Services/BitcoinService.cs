using System.Text.Json;

namespace BitcoinTracker.Services;

public class BitcoinService
{
    private readonly HttpClient httpClient;

    public BitcoinService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<decimal?> GetCurrentPriceAsync()
    {
        try
        {
            var response = await httpClient.GetStringAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd");
            var priceData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(response);
            return priceData!["bitcoin"]["usd"];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching the Bitcoin price: {ex.Message}");
            return null;
        }
    }
}