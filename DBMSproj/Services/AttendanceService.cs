using Dapper;
using Microsoft.Data.SqlClient;
namespace DBMSproj.Services
{
    public class AttendanceService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AttendanceService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection"); // Make sure your connection string name matches
        }

        public async Task<List<AttendanceRecord>> GetAttendanceRecordsAsync(int empId)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT Adate, Status, Atid FROM Attendance WHERE empID = @EmpId";
            var records = await connection.QueryAsync<AttendanceRecord>(query, new { EmpId = empId });
            return records.ToList();
        }

        public async Task AddLeaveApplicationAsync(LeaveApplication leave)
        {
            var random = new Random();
            leave.Leaveid = random.Next(80000, 90001);
            using var connection = new SqlConnection(_connectionString);

            var query = @"INSERT INTO Leave (Leaveid,EmpId, Start_date, End_date, type,  Status)
                          VALUES (@Leaveid, @empid, @Start_date, @End_date, @type, @Status)";

            await connection.ExecuteAsync(query, leave);
        }
    }

    public class AttendanceRecord
    {
        public DateTime ADate { get; set; }
        public string Status { get; set; }
        public int Atid { get; set; }
    }

    public class LeaveApplication
    {
        public int Leaveid { get; set; }
        public int EmpId { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string type { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
