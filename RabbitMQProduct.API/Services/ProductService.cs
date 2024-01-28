using RabbitMQProduct.API.Data;
using RabbitMQProduct.API.Models;

namespace RabbitMQProduct.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _dbContext;

        public ProductService(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(Guid id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            return product is null ? throw new ArgumentNullException(nameof(GetProductById)) : product;
        }

        public Product AddProduct(Product product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(Guid id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException(nameof(GetProductById));
            var result = _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return result != null;
        }
        

        public Product UpdateProduct(Product product)
        {
            var result = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
