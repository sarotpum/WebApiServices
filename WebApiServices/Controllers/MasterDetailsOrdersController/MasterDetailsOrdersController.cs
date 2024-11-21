using Microsoft.AspNetCore.Mvc;
using SharedService.Models.MasterDetailsOrders;
using WebApiServices.BussinessLogic;

namespace WebApiServices.Controllers.MasterDetailsOrdersController
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterDetailsOrdersController : ControllerBase
    {
        private readonly MasterDetailsOrdersLogic _viewModel;

        public MasterDetailsOrdersController(MasterDetailsOrdersLogic viewModel)
        {
            _viewModel = viewModel;
        }

        [HttpGet("GetItemDetails")]
        public IActionResult GetItemDetails()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetItemDetails();
            return Ok(result);
        }

        [HttpGet("GetCustomerDetails")]
        public IActionResult GetCustomerDetails()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.SelectMany(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetCustomerDetails();
            return Ok(result);
        }

        [HttpPost("AddOrderDetail")]
        public IActionResult AddOrdersDetail(OrdersDetailsModel orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.AddOrdersDetail(orderDetail);
            return Ok(result);
        }

        [HttpGet("OrdersDetails")]
        public IActionResult GetOrdersDetails()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetOrdersDetails();
            return Ok(result);
        }

        [HttpGet("OrdersDetail/{id}")]
        public IActionResult GetOrdersDetail(long id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetOrdersDetail(id);
            return Ok(new { result.Item1, result.Item2 });
        }

        [HttpDelete("DeleteOrderDetail/{id}")]
        public IActionResult DeleteOrder(long id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.DeleteOrder(id);
            return Ok(result);
        }
    }
}
