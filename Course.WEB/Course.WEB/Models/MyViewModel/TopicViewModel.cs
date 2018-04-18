using Course.WEB.Models.Entities;
using System.Collections.Generic;

namespace Course.WEB.Models.MyViewModel
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }

        public TopicStatistic TopicStatistic { get; set; }

        public List<TaskStatistic> TasksStatistic { get; set; }
    }
}