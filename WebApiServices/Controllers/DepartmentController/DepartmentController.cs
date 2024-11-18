using Microsoft.AspNetCore.Mvc;
using SharedService.Models.DepartmentModel;
using System.Data;
using WebApiServices.BussinessLogic;

namespace WebApiServices.Controllers.DepartmentController
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentLogic _viewModel;

        public DepartmentController(DepartmentLogic viewModel)
        {
            _viewModel = viewModel;
        }

        [HttpGet("GetDepartment")]
        public IActionResult GetDepartment()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                   ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetDepartment();
            return Ok(result);
        }

        [HttpGet("GetDepartment2")]
        public async Task<IActionResult> GetDepartment2()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                   ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.GetDepartment2();
            return Ok(result);
        }

        [HttpPost("AddDepartment")]
        public IActionResult AddDepartment(DepartmentModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.AddDepartment(value);
            return Ok(result);
        }

        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment(DepartmentModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.UpdateDepartment(value);
            return Ok(result);
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.DeleteDepartment(id);
            return Ok(result);
        }
    }
}
