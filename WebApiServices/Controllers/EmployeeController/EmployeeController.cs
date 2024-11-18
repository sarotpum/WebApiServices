using Microsoft.AspNetCore.Mvc;
using SharedService.Models.Employees;
using WebApiServices.BussinessLogic;

namespace WebApiServices.Controllers.EmployeeController
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeLogic _viewModel;

        public EmployeeController(EmployeeLogic viewModel)
        {
            _viewModel = viewModel;
        }

        [HttpGet("GetEmployee")]
        public IActionResult GetEmployee()
        {
            if (!ModelState.IsValid) 
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(s => s.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetEmployee();
            return Ok(result);
        }

        [HttpGet("GetEmployee2")]
        public async Task<IActionResult> GetEmployee2()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = await _viewModel.GetEmployee2();
            return Ok(result);
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(EmployeeModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, 
                    ModelState.Values.SelectMany(s=> s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.AddEmployee(value);
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeModel value)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.UpdateEmployee(value);
            return Ok(result);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.DeleteEmployee(id);
            return Ok(result);
        }

        [HttpPost("SaveFile")]
        public IActionResult SaveFileEmployee()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.SaveFileEmployee(Request);
            return Ok(result);
        }

        [HttpGet("GetAllDepartmentNames")]
        public IActionResult GetAllDepartmentNames()
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400,
                    ModelState.Values.SelectMany(s => s.Errors.Select(ss => ss.ErrorMessage)).ToArray());
            }

            var result = _viewModel.GetAllDepartmentNames();
            return Ok(result);
        }
    }
}
