namespace DBMSproj.Models
{
    public class TaxDetail
    {
        public int EmployeeId { get; set; }
        public string Month { get; set; } = string.Empty;
        public decimal BasicPay { get; set; }
        public decimal TDS { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal ProvidentFund { get; set; }
    }
}
