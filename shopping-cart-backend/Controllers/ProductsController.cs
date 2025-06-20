using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.Models;
using System.Collections.Generic;

namespace ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Title = "Phone", Category = "Electronics", Price = 10000, ImageUrl = "http://localhost:8080/images/mobile.avif" },
                new Product { Id = 2, Title = "T-Shirt", Category = "Clothing", Price = 500, ImageUrl = "http://localhost:8080/images/tshirt.avif" },
                new Product { Id = 3, Title = "Shoes", Category = "Footwear", Price = 1500, ImageUrl = "http://localhost:8080/images/shoes.png" },
                new Product { Id = 4, Title = "Laptop", Category = "Electronics", Price = 45000, ImageUrl = "http://localhost:8080/images/laptop.jpg" },
                new Product { Id = 5, Title = "Book", Category = "Education", Price = 300, ImageUrl = "http://localhost:8080/images/book.jpg" },
                new Product { Id = 6, Title = "Watch", Category = "Accessories", Price = 2500, ImageUrl = "http://localhost:8080/images/watch.jpg" },
                new Product { Id = 7, Title = "Backpack", Category = "Bags", Price = 1200, ImageUrl = "http://localhost:8080/images/bag.webp" },
                new Product { Id = 8, Title = "Headphones", Category = "Electronics", Price = 2000, ImageUrl = "http://localhost:8080/images/headphones.jpg" },
                new Product { Id = 9, Title = "Keyboard", Category = "Electronics", Price = 1500, ImageUrl = "http://localhost:8080/images/keyboard.jpg" },
                new Product { Id = 10, Title = "Mug", Category = "Kitchen", Price = 250, ImageUrl = "http://localhost:8080/images/mug.jpg" }
            };

            return Ok(products);
        }
    }
}
