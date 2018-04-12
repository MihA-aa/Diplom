using Course.WEB.Models.Entities;
using System.Collections.Generic;

namespace Course.WEB.Models.MyViewModel
{
    public class TopicTasksViewModel
    {
        public Topic Toipc { get; set; }

        public List<Task> Task { get; set; }
    }
}