using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_BookStore.Interfaces;
using API_BookStore.DTOs;
using API_BookStore.Models;
using Azure.Messaging;
using API_BookStore.DTOs.EmployeeDto;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee employeeService;
        public EmployeesController(IEmployee employee)
        {
            employeeService = employee;
        }
        [HttpGet("get-employee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var result = await employeeService.GetEmployeeAsync();
                if (result == null)
                    return Ok(new
                    {
                        status = -1,
                        messages = "Không có nhân viên"
                    });
                return Ok(new
                {
                    status = 1,
                    messages = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = -1,
                    messages = ex.ToString()
                });
            }
        }

        [HttpGet("get-employee/{employeeCode}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(string employeeCode)
        {
            try
            {

                var employeeDetail = await employeeService.GetEmployeeByIDAsync(employeeCode);
                if (employeeDetail == null)
                    return Ok(new
                    {
                        status = -1,
                        messages = "Lỗi hiển thị chi tiết nhân viên"
                    });
                return Ok(new
                {
                    status = 1,
                    messages = employeeDetail
                });

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = -1,
                    messages = ex.ToString()
                });
            }
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                var result = await employeeService.CreateEmployeeAsync(createEmployeeDto);
                if (result == null)
                {
                    return Ok(new
                    {
                        status = -1,
                        messages = "Không có nhân viên được thêm"
                    });
                }
                return Ok(new
                {
                    status = 1,
                    messages = "Thêm nhân viên thành công"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = -1,
                    messages = ex.Message
                });
            }

        }

        [HttpPut("edit-employee/{employeeCode}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto, string employeeCode)
        {
            try
            {
                if (employeeCode == null)
                    return Ok(new
                    {
                        status = -1,
                        messages = "Mã nhân viên không tồn tại"
                    });
                var employeeUpdate = await employeeService.UpdateEmployeeAsync(updateEmployeeDto, employeeCode);
                if (employeeUpdate == null)
                    return Ok(new
                    {
                        status = -1,
                        messages = "Cập nhật nhân viên không thành công"
                    });
                return Ok(new
                {

                    status = 1,
                    messages = "Cập nhật nhân viên thành công"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = -1,
                    messages = ex.Message
                });
            }
        }

        [HttpDelete("delete-employee/{employeeCode}")]
        public async Task<IActionResult> DeleteEmployee( string employeeCode)
        {
            if (employeeCode == null)
                return Ok(new
                {
                    status = -1,
                    message = "Không có nhân viên này"
                });
            var result = await employeeService.DeleteEmployeeAsync(employeeCode);
            if (result.Equals(false))
                return Ok(new { 
                    status = -1,
                    messages = "Lỗi xóa nhân viên"
                });
            return Ok(new
            {
                status = 1,
                messages = "Xóa thành công nhân viên"
            });

        }
    }
}
