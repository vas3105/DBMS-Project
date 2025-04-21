namespace DBMSproj.Models
{
    public class Payslip
    {
        public int payslipID { get; set; }
        public int EmployeeID { get; set; }
        public decimal basic_salary { get; set; }
        public decimal deduction { get; set; }
        public decimal net_salary { get; set; }
        public DateTime payment_date { get; set; }
        public string TransactionID { get; set; }
    }
}