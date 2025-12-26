using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_BookStore.Models;
using API_BookStore.DTOs.EmployeeDto;

namespace API_BookStore.Interfaces
{
    public interface IEmployee
    {
        Task<List<EmployeeListDto?>> GetEmployeeAsync();
        Task<Employee?> GetEmployeeByIDAsync (string employeeCode);
        Task<Employee?> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto);
        Task<Employee?> UpdateEmployeeAsync (UpdateEmployeeDto updateEmployeeDto, string employeeCode);
        Task<bool> DeleteEmployeeAsync (string employeeCode);

    }
}
