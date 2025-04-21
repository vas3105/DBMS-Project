using Dapper;
using Microsoft.Data.SqlClient;

namespace DBMSproj.Services
{
    public class EmployeeService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string empId)
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                SELECT 
                    EmpId AS EmployeeId,
                    Name,
                    PhoneNo,
                    EmailID AS PersonalEmail,
                    Dept AS Department,
                    Designation,
                    dob AS DateOfBirth,
                    doj AS DateOfJoining,
                    office_email AS OfficeEmail
                FROM Employee
                WHERE EmpId = @EmpId
            ";

            return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { EmpId = empId });
        }
    }

    public class Employee
    {
        public string EmployeeId { get; set; } = "";
        public string Name { get; set; } = "";
        public string PhoneNo { get; set; } = "";
        public string PersonalEmail { get; set; } = "";
        public string OfficeEmail { get; set; } = "";
        public string Department { get; set; } = "";
        public string Designation { get; set; } = "";
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfBirth { get; set; }
       // public string TemporaryAddress { get; set; } = "";
       // public string PermanentAddress { get; set; } = "";
    }
}
