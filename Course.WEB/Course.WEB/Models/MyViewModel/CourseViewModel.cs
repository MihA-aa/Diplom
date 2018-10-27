using System.Collections.Generic;
using Course.WEB.Models.Entities;
using de = Course.WEB.Models.Entities;

namespace Course.WEB.Models.MyViewModel
{
    public class CourseViewModel
    {
        public de.Course Course { get; set; }

        public List<TopicStatistic> TopicsStatistic { get; set; }
    }
}