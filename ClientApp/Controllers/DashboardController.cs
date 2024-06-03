using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

public class DashboardController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DashboardController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var orderServiceStatus = await CheckHealth("https://localhost:7040/health");
        var productServiceStatus = await CheckHealth("https://localhost:7218/health");

        ViewBag.OrderServiceStatus = orderServiceStatus.Status;
        ViewBag.OrderServiceLastChecked = orderServiceStatus.LastChecked;
        ViewBag.ProductServiceStatus = productServiceStatus.Status;
        ViewBag.ProductServiceLastChecked = productServiceStatus.LastChecked;

        return View();
    }

    private async Task<(string Status, string LastChecked)> CheckHealth(string url)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return ("Up", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                return ("Down", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        catch
        {
            return ("Down", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}