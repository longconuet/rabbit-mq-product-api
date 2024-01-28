using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQProduct.API.Models;
using RabbitMQProduct.API.RabbitMQ;
using RabbitMQProduct.API.Services;

namespace RabbitMQProduct.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public ProductController(IProductService productService, IRabbitMQProducer rabbitMQProducer)
        {
            _productService = productService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("list")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProductList();
        }

        [HttpGet("product-by-id/{id}")]
        public Product GetProductById(Guid id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost("add")]
        public Product AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            var productData = _productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabbitMQProducer.SendProductMessage(productData);
            return productData;
        }

        [HttpPut("update")]
        public Product UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [HttpDelete("delete")]
        public bool DeleteProduct(Guid Id)
        {
            return _productService.DeleteProduct(Id);
        }
    }
}
