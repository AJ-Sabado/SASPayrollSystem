namespace DomainLayer.Models.EmployeeDeduction
{
    public class EmployeeDeductionModel
    {
        //Key
        //Nav

        public decimal WHTax { get; set; }
        public decimal SSS { get; set; }
        public decimal PhilHealth { get; set; }
        public decimal PagIbig { get; set; }
        public decimal Late { get; set; }
        public decimal Loan { get; set; }
        public decimal Others { get; set; }

        public decimal GetTotalDeduction() 
        {
            return WHTax + SSS + PhilHealth + PagIbig + Late + Loan + Others;
        }
    }
}
