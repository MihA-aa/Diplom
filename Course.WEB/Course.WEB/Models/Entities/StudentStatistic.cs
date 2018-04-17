using System.ComponentModel.DataAnnotations;

namespace Course.WEB.Models.Entities
{
    public class StudentStatistic
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public decimal PartOfCorrectAnswers { get; set; }

        public decimal PartOfIncorrectAnswers { get; set; }

        public decimal LogitOfLevelKnowledge { get; set; }
    }
}