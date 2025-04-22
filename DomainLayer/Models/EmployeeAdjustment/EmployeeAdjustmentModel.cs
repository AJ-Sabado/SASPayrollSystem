namespace DomainLayer.Models.EmployeeAdjustment
{
    public class EmployeeAdjustmentModel
    {
        //Key
        //Nav

        public decimal Month13TH { get; set; }
        public decimal Incentive { get; set; }
        public decimal PaidLeaves { get; set; }
        public decimal HolidayPay { get; set; }
        public decimal Others { get; set; }

        public decimal GetTotalAdjustment()
        {
            return Month13TH + Incentive + PaidLeaves + HolidayPay + Others;
        }
    }
}
