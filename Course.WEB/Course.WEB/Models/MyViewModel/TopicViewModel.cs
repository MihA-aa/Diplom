using System.Collections.Generic;
using Course.WEB.Models.Entities;

namespace Course.WEB.Models.MyViewModel
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }

        public TopicStatistic TopicStatistic { get; set; }

        public List<TaskStatistic> TasksStatistic { get; set; }

        public Dictionary<int, bool> SolvedTasks { get; set; }
    }
}