namespace Course.WEB.Models.Entities
{
    public class TopicStatistic
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public int OverallAcademicPerformance { get; set; }

        public decimal Variation { get; set; }

        public decimal Dispersion { get; set; }

        public decimal StandartDeviation { get; set; }

        public decimal CorrelationСoefficient { get; set; }

        public decimal AverageComplexity { get; set; }
        
        public int AverageTimeForTopic { get; set; }

        public int AverageTimeForTask { get; set; }
    }
}