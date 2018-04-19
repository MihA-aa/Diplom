using System.Collections.Generic;

namespace Course.WEB.Models.MyViewModel
{
    public class StudentStatisticForTopic
    {
        public string Initials { get; set; }

        public Dictionary<int, bool?> Tasks { get; set; }

        public int Points { get; set; }
    }
}