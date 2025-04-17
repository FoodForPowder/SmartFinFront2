namespace SmartFinFront.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public decimal? ExpenseLimit { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
