using Dapper;
using DBMSproj.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBMSproj.Services
{
    public class PayslipService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PayslipService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Payslip>> GetPayslipsByEmployeeIdAsync(int empId)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                SELECT 
                    payslipID,
                    EmployeeID,
                    basic_salary,
                    deduction,
                    net_salary,
                    payment_date,
                    TransactionID
                FROM Payslip
                WHERE EmployeeID = @EmpId
                ORDER BY payment_date DESC
            ";

            var result = await connection.QueryAsync<Payslip>(query, new { EmpId = empId });
            return result.AsList();
        }
    }
}