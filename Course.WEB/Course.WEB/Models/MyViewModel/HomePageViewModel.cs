using System.Collections.Generic;
using Course.WEB.Models.Entities;

namespace Course.WEB.Models.MyViewModel
{
    public class HomePageViewModel
    {
        public List<Discipline> Disciplines { get; set; }

        public int CountOfCourses { get; set; }

        public int CountOfTopics { get; set; }

        public int CountOfTasks { get; set; }
    }
}