using Course.WEB.Models.Entities;
using System.Collections.Generic;

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