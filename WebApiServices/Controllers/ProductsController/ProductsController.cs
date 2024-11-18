using Microsoft.AspNetCore.Mvc;
using SharedService.Models.Products;
using WebApiServices.BussinessLogic;

namespace WebApiServices.Controllers.ProductsController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsLogic _viewModel;

        public ProductsController(ProductsLogic viewModel)
        {
            _viewModel = viewModel;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.GetProducts();
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> AddProduct(ProductModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.AddProduct(value);
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> UpdateProduct(ProductModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.UpdateProduct(value);
            return Ok(result);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.DeleteProduct(id);
            return Ok(result);
        }
    }
}
