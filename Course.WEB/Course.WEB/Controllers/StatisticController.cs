using Course.WEB.Models.MyViewModel;
using Course.WEB.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace Course.WEB.Controllers
{
    [Authorize]
    public class StatisticController : Controller
    {
        readonly EFUnitOfWork db = new EFUnitOfWork();

        public ActionResult Topic(int? topicId)
        {
            if (topicId == null)
                return HttpNotFound();
            var topic = db.Topics.Get(topicId.Value);
            if (topic == null)
                return HttpNotFound();
            var topicStatistic = db.TopicStatistics.Find(x => x.TopicId == topicId).FirstOrDefault();
            if (topicStatistic == null)
                return HttpNotFound();

            var taskIds = topic.Tasks.Select(x => x.Id);
            var tasksStatistic = db.TaskStatistics.Find(x => taskIds.Contains(x.TaskId)).ToList();
            var ratings = db.Ratings.Find(x => taskIds.Contains(x.TaskId))
                .GroupBy(x => new { x.StudentId, x.TaskId }, (key, g) => g.OrderBy(e => e.DateOfSolution).FirstOrDefault());

            var studentStatistics = ratings.Select(x =>
            {
                var dict = new Dictionary<int, bool?>();
                taskIds.ForEach(y => dict.Add(y, null));
                ratings.Where(y => y.StudentId == x.StudentId).ForEach(y => dict[y.TaskId] = y.IsSolved);

                return new StudentStatisticForTopic
                {
                    Initials = x.Student.FirstName + " " + x.Student.LastName,
                    UserId = x.StudentId,
                    Tasks = dict,
                    Points = ratings
                    .Where(y => y.StudentId == x.StudentId)
                    .Where(y => y.IsSolved)
                    .Select(y => topic.Tasks.FirstOrDefault(z => z.Id == y.TaskId).Weight)
                    .Sum()
                };
            }).GroupBy(x => x.Initials, (key, g) => g.FirstOrDefault()).ToList();

            var viewModel = new TopicStatisticViewModel
            {
                Topic = topic,
                TopicStatistic = topicStatistic,
                TasksStatistic = tasksStatistic,
                StudentStatistics = studentStatistics
            };

            return View(viewModel);
        }
    }
}