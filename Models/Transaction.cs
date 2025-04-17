namespace SmartFinFront.Models
{
    public class Transaction
    {
        public int id { get; set; }

        public decimal sum { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public int? CategoryId { get; set; } = 0; 
    }
}
