using Bogus;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductRepository
    {
        private List<Product> products;

        public ProductRepository()
        {
            var faker = new Faker<Product>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => Convert.ToDecimal((f.Commerce.Price())));

            products = faker.Generate(1000);
        }

        public IEnumerable<Product> GetAllProducts(int pageNumber, int pageSize)
        {
            return products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
    }
}