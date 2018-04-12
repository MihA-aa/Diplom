using Course.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.WEB.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int ActualTime { get; set; }
        public decimal ActualComplexity { get; set; }
        public int? TaskId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Task Task { get; set; }
    }
}
