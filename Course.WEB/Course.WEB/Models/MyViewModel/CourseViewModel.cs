using de = Course.WEB.Models.Entities;
using Course.WEB.Models.Entities;
using System.Collections.Generic;

namespace Course.WEB.Models.MyViewModel
{
    public class CourseViewModel
    {
        public de.Course Course { get; set; }

        public List<TopicStatistic> TopicsStatistic { get; set; }
    }
}