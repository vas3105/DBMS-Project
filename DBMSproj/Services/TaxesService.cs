using Dapper;
using Microsoft.Data.SqlClient;
using DBMSproj.Models;

namespace DBMSproj.Services
{
    public class TaxesService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TaxesService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<TaxDetail>> GetTaxDetailsByEmployeeIdAsync(int empId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"
                SELECT 
                    EmpId AS EmployeeId,
                    Month,
                    BasicPay,
                    TDS,
                    ProfessionalTax,
                    ProvidentFund
                FROM TaxDetails
                WHERE EmpId = @EmpId
            ";
            var taxDetails = await connection.QueryAsync<TaxDetail>(query, new { EmpId = empId });
            return taxDetails.ToList();
        }
    }
}
