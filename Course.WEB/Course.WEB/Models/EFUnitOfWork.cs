using System;
using Course.WEB.Models.Interfaces;
using Course.WEB.Models.Entities;
using de = Course.WEB.Models.Entities;

namespace Course.WEB.Models.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext db;
        private CourseRepository courseRepository;
        private DisciplineRepository disciplineRepository;
        private RatingRepository ratingRepository;
        private TaskRepository taskRepository;
        private TopicRepository topicRepository;
        private TaskStatisticRepository taskStatisticRepository;
        private StudentStatisticRepository studentStatisticRepository;
        private TopicStatisticRepository topicStatisticRepository;

        public EFUnitOfWork()
        {
            db = new ApplicationDbContext();
        }

        public IRepository<de.Course> Courses
        {
            get
            {
                if (courseRepository == null)
                    courseRepository = new CourseRepository(db);
                return courseRepository;
            }
        }

        public IRepository<Discipline> Disciplines
        {
            get
            {
                if (disciplineRepository == null)
                    disciplineRepository = new DisciplineRepository(db);
                return disciplineRepository;
            }
        }

        public IRepository<Rating> Ratings
        {
            get
            {
                if (ratingRepository == null)
                    ratingRepository = new RatingRepository(db);
                return ratingRepository;
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                if (taskRepository == null)
                    taskRepository = new TaskRepository(db);
                return taskRepository;
            }
        }

        public IRepository<Topic> Topics
        {
            get
            {
                if (topicRepository == null)
                    topicRepository = new TopicRepository(db);
                return topicRepository;
            }
        }

        public IRepository<TaskStatistic> TaskStatistics
        {
            get
            {
                if (taskStatisticRepository == null)
                    taskStatisticRepository = new TaskStatisticRepository(db);
                return taskStatisticRepository;
            }
        }
        
        public IRepository<StudentStatistic> StudentStatistics
        {
            get
            {
                if (studentStatisticRepository == null)
                    studentStatisticRepository = new StudentStatisticRepository(db);
                return studentStatisticRepository;
            }
        }

        public IRepository<TopicStatistic> TopicStatistics
        {
            get
            {
                if (topicStatisticRepository == null)
                    topicStatisticRepository = new TopicStatisticRepository(db);
                return topicStatisticRepository;
            }
        }
        
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
