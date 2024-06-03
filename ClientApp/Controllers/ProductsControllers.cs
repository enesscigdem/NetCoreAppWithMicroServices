using Microsoft.AspNetCore.Mvc;
using ClientApp.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ProductService.Data; // Add this for ProductRepository
using System.Linq.Dynamic.Core; // Add this for dynamic LINQ

namespace ClientApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ProductRepository _productRepository; // Add this for repository

        public ProductsController(IHttpClientFactory httpClientFactory, ProductRepository productRepository)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5236/api/");
            _productRepository = productRepository; // Initialize repository
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/products?pageNumber={pageNumber}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseContent);
                ViewBag.ProductServiceStatus = "Up";
                ViewBag.CurrentPage = pageNumber;
                ViewBag.PageSize = pageSize;
                return View(products);
            }
            catch (HttpRequestException)
            {
                ViewBag.ProductServiceStatus = "Down";
                return View(new List<Product>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/products", product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(responseContent);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/products/{id}", product);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(responseContent);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = HttpContext.Request.Form["start"].FirstOrDefault();
            var length = HttpContext.Request.Form["length"].FirstOrDefault();
            var sortColumn = HttpContext.Request
                .Form["columns[" + HttpContext.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDirection = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = HttpContext.Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var response = await _httpClient.GetAsync($"/products");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(responseContent).AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                products = products.Where(m => m.Name.Contains(searchValue));
            }

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                products = products.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            int recordsTotal = products.Count();
            var data = products.Skip(skip).Take(pageSize).ToList();

            var jsonData = new
                { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
    }
}