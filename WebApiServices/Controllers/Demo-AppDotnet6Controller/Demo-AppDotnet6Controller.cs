using Microsoft.AspNetCore.Mvc;

namespace WebApiServices.Controllers.Demo_AppDotnet6Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Demo_AppDotnet6Controller : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> GetProduct()
        {
            var products = new List<string>();
            products.Add("VueJs");
            products.Add("Flutter");
            products.Add("React");

            return Ok(products);
        }

        [HttpGet("{id}")] // .../12
        public ActionResult GetProductById(int id)
        {
            return Ok(new { productId = id, name ="VueJs" });
        }

        [HttpGet("search/{id}/{category}")] // .../12
        public ActionResult SearchProductById(int id, string category)
        {
            return Ok(new { productId = id, name = "VueJs", category = category });
        }

        [HttpGet("query/product")] // .../query/product?id=12345&cat=web
        public ActionResult QueryProductById([FromQuery] string id, [FromQuery] string category)
        {
            return Ok(new { productId = id, name = "VueJs", category = category });
        }

        [HttpGet("query/product/{user}")] // .../query/product ?id=123&cat=web
        public ActionResult QueryProductById([FromQuery] string id, [FromQuery] string category, string user)
        {
            return Ok(new { productId = id, name = "VueJs", category = category, user = user });
        }

        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] Product product)
        {
            return Ok(product);
        }

        [HttpPost("add")]
        public ActionResult<Product> AddProduct2([FromBody] Product product)
        {
            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductById(int id, [FromBody] Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            if (id != 1111)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductById(int id)
        {
            if (id != 1111)
                return NotFound();

            return NoContent();
        }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int price { get; set; }
    }
}
