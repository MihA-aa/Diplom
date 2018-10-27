using System;
using Course.WEB.Models.Entities;
using Course.WEB.Models.Interfaces;
using Course.WEB.Models.Repositories;
using de = Course.WEB.Models.Entities;

namespace Course.WEB.Models
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;
        private CourseRepository courseRepository;
        private DisciplineRepository disciplineRepository;
        private RatingRepository ratingRepository;
        private TaskRepository taskRepository;
        private TopicRepository topicRepository;
        private TaskStatisticRepository taskStatisticRepository;
        private StudentStatisticRepository studentStatisticRepository;
        private TopicStatisticRepository topicStatisticRepository;
        private bool disposed;

        public EFUnitOfWork()
        {
            db = new ApplicationDbContext();
        }

        public IRepository<de.Course> Courses => courseRepository ?? (courseRepository = new CourseRepository(db));

        public IRepository<Discipline> Disciplines => disciplineRepository ?? (disciplineRepository = new DisciplineRepository(db));

        public IRepository<Rating> Ratings => ratingRepository ?? (ratingRepository = new RatingRepository(db));

        public IRepository<Task> Tasks => taskRepository ?? (taskRepository = new TaskRepository(db));

        public IRepository<Topic> Topics => topicRepository ?? (topicRepository = new TopicRepository(db));

        public IRepository<TaskStatistic> TaskStatistics => taskStatisticRepository ?? (taskStatisticRepository = new TaskStatisticRepository(db));

        public IRepository<StudentStatistic> StudentStatistics => studentStatisticRepository ?? (studentStatisticRepository = new StudentStatisticRepository(db));

        public IRepository<TopicStatistic> TopicStatistics => topicStatisticRepository ?? (topicStatisticRepository = new TopicStatisticRepository(db));

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
