using Microsoft.AspNetCore.Mvc;
using Syncfy.Models;

namespace Syncfy.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase {
        private static readonly List<Product> products = new List<Product> {
            new Product { Name = "Laptop", Price = 1000 },
            new Product { Name = "Smartphone", Price = 500},
            new Product { Name = "Tablet", Price = 300}
        };

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product) {
            products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id) {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) {
                return NotFound();
            }
            return product;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts() {
            return products;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product product) {
            var productIndex = products.FindIndex(p => p.Id == id);
            if (productIndex == -1) {
                return NotFound();
            }

            products[productIndex] = product;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id) {
            var productIndex = products.FindIndex(p => p.Id == id);
            if (productIndex == -1) {
                return NotFound();
            }

            products.RemoveAt(productIndex);
            return NoContent();
        }



        [HttpGet("{name}")]
        public ActionResult<decimal> GetPriceByName(string name) {
            var product = products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            if(product == null) {
                return NotFound("Product not found");
            }

            return product.Price;
        }
    }
}
