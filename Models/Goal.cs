namespace SmartFinFront.Models
{
    public class Goal
    {
        public int id { get; set; }


        public DateTime? dateOfStart { get; set; }

        public DateTime? dateOfEnd { get; set; } 

        public decimal payment { get; set; } = 0;

        public string name { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public decimal plannedSum { get; set; } = 0;

        public decimal currentSum { get; set; } = 0;

        public string status { get; set; } = string.Empty;

        public int UserId { get; set; }

        public decimal lastMonthContribution { get; set; }
        public DateTime lastContributionDate { get; set; }
    }
}
