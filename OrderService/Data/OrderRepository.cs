using Bogus;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderRepository
    {
        private List<Order> orders;

        public OrderRepository()
        {
            var faker = new Faker<Order>()
                .RuleFor(o => o.Id, f => f.IndexFaker + 1)
                .RuleFor(o => o.ProductName, f => f.Commerce.ProductName())
                .RuleFor(o => o.Quantity, f => f.Random.Int(1, 100));

            orders = faker.Generate(1000);
        }

        public IEnumerable<Order> GetAllOrders(int pageNumber, int pageSize)
        {
            return orders.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public Order GetOrderById(int id)
        {
            return orders.FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            order.Id = orders.Max(o => o.Id) + 1;
            orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.ProductName = order.ProductName;
                existingOrder.Quantity = order.Quantity;
            }
        }

        public void DeleteOrder(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                orders.Remove(order);
            }
        }
    }
}