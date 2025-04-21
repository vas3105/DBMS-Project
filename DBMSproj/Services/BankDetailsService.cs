using Dapper;
using Microsoft.Data.SqlClient; 
namespace DBMSproj.Services
{
    public class BankDetailsService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public BankDetailsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection"); // Must match your appsettings.json
        }

        public async Task<BankInfo?> GetBankDetailsAsync(int empId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"SELECT 
                A_Name AS AccountHolderName, 
                A_Number AS AccountNumber, 
                IFSC AS IFSCCode, 
                Bank_Name AS BankName, 
                Branch 
             FROM Bank_Details 
             WHERE EmpId = @EmpId";

            return await connection.QueryFirstOrDefaultAsync<BankInfo>(query, new { EmpId = empId });
        }
        //disabled the below function
        public async Task InsertOrUpdateBankDetailsAsync(int empId, BankInfo bankInfo)
        {
            using var connection = new SqlConnection(_connectionString);

            var checkQuery = "SELECT COUNT(*) FROM Bank_Details WHERE EmpId = @EmpId";
            var exists = await connection.ExecuteScalarAsync<int>(checkQuery, new { EmpId = empId }) > 0;

            if (exists)
            {
                var updateQuery = @"UPDATE Bank_Details 
                                    SET A_Name = @A_Name, A_Number = @A_Number, IFSC = @IFSC, Bank_Name = @Bank_Name, Branch = @Branch
                                    WHERE EmpId = @EmpId";
                await connection.ExecuteAsync(updateQuery, new { bankInfo.AccountHolderName, bankInfo.AccountNumber, bankInfo.IFSCCode, bankInfo.BankName, bankInfo.Branch, EmpId = empId });
            }
            else
            {
                var insertQuery = @"INSERT INTO Bank_Details (EmpId, A_Name, A_Number, IFSC, Bank_Name, Branch)
                                    VALUES (@EmpId, @AccountHolderName, @AccountNumber, @IFSCCode, @BankName, @Branch)";
                await connection.ExecuteAsync(insertQuery, new { EmpId = empId, bankInfo.AccountHolderName, bankInfo.AccountNumber, bankInfo.IFSCCode, bankInfo.BankName, bankInfo.Branch });
            }
        }
    }

    public class BankInfo
    {
        public string AccountHolderName { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string IFSCCode { get; set; } = "";
        public string BankName { get; set; } = "";
        public string Branch { get; set; } = "";
    }
}
