using System.ComponentModel.DataAnnotations;

namespace Course.WEB.Models.Entities
{
    public class TaskStatistic
    {
        [Key]
        public int TaskId { get; set; }

        public int SolvedCount { get; set; }

        public int SolvedCorrectCount { get; set; }

        public decimal PartOfCorrectAnswers { get; set; }

        public decimal PartOfIncorrectAnswers { get; set; }

        public decimal LogitOfTaskDifficulty { get; set; }

        public decimal Variation { get; set; }

        public decimal Dispersion { get; set; }

        public decimal StandartDeviation { get; set; }

        public decimal CorrelationСoefficient { get; set; }
    }
}