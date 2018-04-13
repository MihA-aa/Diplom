using System;

namespace Course.WEB.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }

        public int ActualTime { get; set; }

        public int TaskId { get; set; }

        public string ApplicationUserId { get; set; }

        public bool IsSolved { get; set; }

        public DateTime DateOfSolution { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Task Task { get; set; }
    }
}
