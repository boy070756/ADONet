using Microsoft.AspNetCore.Mvc;


namespace APIEntity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // Giả sử có một danh sách sản phẩm trong RAM (demo)
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500 },
            new Product { Id = 2, Name = "Phone", Price = 800 }
        };

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Không tìm thấy sản phẩm với Id = {id}");
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1; // tự tăng Id
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Không tìm thấy sản phẩm với Id = {id}");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound($"Không tìm thấy sản phẩm với Id = {id}");

            _products.Remove(product);
            return NoContent(); // 204
        }
    }

    // Model Product
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    //  fetch("http://localhost:5000/api/products")
    //.then(response => response.json())
    //.then(data => console.log(data))
    //.catch(error => console.error("Error:", error));


    //  fetch("http://localhost:5000/api/products/1")
    //.then(response => {
    //      if (!response.ok) throw new Error("Not found");
    //      return response.json();
    //  })
    //.then(product => console.log(product))
    //.catch(error => console.error("Error:", error));



    //    fetch("http://localhost:5000/api/products", {
    //    method: "POST",
    //  headers:
    //        {
    //            "Content-Type": "application/json"
    //  },
    //  body: JSON.stringify({
    //        name: "Tablet",
    //    price: 500
    //  })
    //})
    //  .then(response => response.json())
    //  .then(newProduct => console.log("Created:", newProduct))
    //  .catch(error => console.error("Error:", error));


    //    fetch("http://localhost:5000/api/products/1", {
    //    method: "PUT",
    //  headers:
    //        {
    //            "Content-Type": "application/json"
    //  },
    //  body: JSON.stringify({
    //        name: "Laptop Gaming",
    //    price: 2000
    //  })
    //})
    //  .then(response => response.json())
    //  .then(updatedProduct => console.log("Updated:", updatedProduct))
    //  .catch(error => console.error("Error:", error));


    //    fetch("http://localhost:5000/api/products/1", {
    //    method: "DELETE"
    //})
    //  .then(response => {
    //        if (response.status === 204)
    //        {
    //            console.log("Deleted successfully");
    //        }
    //        else
    //        {
    //            console.error("Delete failed");
    //        }
    //    })
    //  .catch(error => console.error("Error:", error));
}
