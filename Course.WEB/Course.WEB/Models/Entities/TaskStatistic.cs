namespace Course.WEB.Models.Entities
{
    public class TaskStatistic
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int SolvedCount { get; set; }

        public int SolvedCorrectCount { get; set; }

        public decimal PartOfCorrectAnswers { get; set; }

        public decimal PartOfIncorrectAnswers { get; set; }

        public decimal LogitOfTaskDifficulty { get; set; }
    }
}