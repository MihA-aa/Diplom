using System;
using Course.WEB.Models.Entities;
using Course.WEB.Models.Entities.Graphics;
using de = Course.WEB.Models.Entities;

namespace Course.WEB.Models.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<de.Course> Courses { get; }

        IRepository<Discipline> Disciplines { get; }

        IRepository<Rating> Ratings { get; }

        IRepository<Task> Tasks { get; }

        IRepository<Topic> Topics { get; }

        IRepository<GraphicTask> GraphicTasks { get; }

        void Save();
    }
}
