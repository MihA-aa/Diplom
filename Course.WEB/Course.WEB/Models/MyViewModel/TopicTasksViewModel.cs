using Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course.WEB.Models.MyViewModel
{
    public class TopicTasksViewModel
    {
        public Topic Toipc { get; set; }
        public List<Task> Task { get; set; }
    }
}