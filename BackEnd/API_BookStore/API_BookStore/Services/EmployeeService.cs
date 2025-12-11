using API_BookStore.DTOs;
using API_BookStore.DTOs.EmployeeDto;
using API_BookStore.Interfaces;
using API_BookStore.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;  

namespace API_BookStore.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly MyDbContext _mydbcontex;
        private readonly IDbConnection _connection;
        public EmployeeService(MyDbContext myDbContext, IDbConnection dbConnection)
        {
            _connection = dbConnection;
            _mydbcontex = myDbContext;
        }

        public async Task<List<EmployeeListDto?>> GetEmployeeAsync()
        {
            var sql = "SELECT * FROM Employee ORDER BY EmployeeCode";
            var result = await _connection.QueryAsync<EmployeeListDto>(sql);
            return result.ToList();
        }

        public async Task<EmployeeListDto?> GetEmployeeByIDAsync(string employeeCode)
        {
            var sql = "SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            var list = await _connection.QueryFirstAsync<EmployeeListDto>(sql, new { EmployeeCode = employeeCode });

            return list;

        }


        public async Task<Employee?> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            try
            {

                using var transaction = await _mydbcontex.Database.BeginTransactionAsync();

                var result = _mydbcontex.Employees.FirstOrDefault(x => x.EmployeeCode == createEmployeeDto.EmployeeCode);

                if (result != null)
                {
                    return null;
                }

                var newEmployee = new Employee
                {
                    EmployeeCode = createEmployeeDto.EmployeeCode,
                    EmployeeName = createEmployeeDto.EmployeeName,
                    Gender = createEmployeeDto.Gender,
                    BirthDay = createEmployeeDto.Birthday,
                    EmployeeAddress = createEmployeeDto.EmployeeAddress,
                    Email = createEmployeeDto.Email
                };

                _mydbcontex.Employees.Add(newEmployee);
                await _mydbcontex.SaveChangesAsync();
                Console.WriteLine(newEmployee);
                await transaction.CommitAsync();
                return newEmployee;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Employee?> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployeeDto, string employeeCode)
        {
            const string sql = @"
            UPDATE Employees 
            SET EmployeeName = @EmployeeName,
                Gender = @Gender,
                BirthDay = @Birthday,
                EmployeeAddress = @EmployeeAddress,
                Email = @Email
            WHERE EmployeeCode = @EmployeeCode;

            SELECT * FROM Employees WHERE EmployeeCode = @EmployeeCode;
        ";

            try
            {
                
                var employee = await _connection.QueryFirstOrDefaultAsync<Employee>(sql, new
                {
                    updateEmployeeDto.EmployeeName,
                    updateEmployeeDto.Gender,
                    updateEmployeeDto.Birthday,
                    updateEmployeeDto.EmployeeAddress,
                    updateEmployeeDto.Email,
                    EmployeeCode = employeeCode
                });

                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(string employeeCode)
        {
            try
            {
                using var transaction = _mydbcontex.Database.BeginTransaction();
                var checkEmployeeCode = await _mydbcontex.Employees.FirstOrDefaultAsync(emp => emp.EmployeeCode == employeeCode);
                if (checkEmployeeCode == null)
                    return false;
                _mydbcontex.Employees.Remove(checkEmployeeCode);
                await _mydbcontex.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
