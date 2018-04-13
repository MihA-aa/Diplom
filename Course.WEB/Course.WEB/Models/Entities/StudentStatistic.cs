using System.ComponentModel.DataAnnotations;

namespace Course.WEB.Models.Entities
{
    public class StudentStatistic
    {
        [Key]
        public int StudentId { get; set; }

        public decimal PartOfCorrectAnswers { get; set; }

        public decimal PartOfIncorrectAnswers { get; set; }

        public decimal LogitOfTaskDifficulty { get; set; }
    }
}