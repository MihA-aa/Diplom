using Course.WEB.Models;
using Course.WEB.Models.Entities;
using de = Course.WEB.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.WEB.Models.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<de.Course> Courses { get; }
        IRepository<Discipline> Disciplines { get; }
        IRepository<Rating> Ratings { get; }
        IRepository<de.Task> Tasks { get; }
        IRepository<Topic> Topics { get; }

        void Save();
    }
}
