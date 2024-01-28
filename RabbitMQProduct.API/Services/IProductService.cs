using RabbitMQProduct.API.Models;

namespace RabbitMQProduct.API.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProductList();
        public Product GetProductById(Guid id);
        public Product AddProduct(Product product);
        public Product UpdateProduct(Product product);
        public bool DeleteProduct(Guid id);
    }
}
