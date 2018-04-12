using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Course.WEB.Models;
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
