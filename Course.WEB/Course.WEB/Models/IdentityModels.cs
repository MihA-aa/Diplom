using de = Course.WEB.Models.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Course.WEB.Models.Entities;
using System.Collections.Generic;

namespace Course.WEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<de.Task> Tasks { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<de.Course> Courses { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }

        public ApplicationUser()
        {
            Tasks = new List<de.Task>();
            Ratings = new List<Rating>();
            Courses = new List<de.Course>();
            Disciplines = new List<Discipline>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EducationalCourse", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new StoreDbInitializer());
        }
        public DbSet<de.Course> Courses { get; set; }

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<de.Task> Tasks { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<TaskStatistic> TaskStatistics { get; set; }

        public DbSet<StudentStatistic> StudentStatistics { get; set; }

        public DbSet<TopicStatistic> TopicStatistics { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {

            #region Roles
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            var superAdminRole = new IdentityRole { Name = "superAdmin" };
            var adminRole = new IdentityRole { Name = "admin" };
            var userRole = new IdentityRole { Name = "user" };
            
            roleManager.Create(superAdminRole);
            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            #endregion
            #region Users

            var mihail = new ApplicationUser
            {
                Id = "65c13a33-5a3e-450c-bd46-503f878e929d",
                LastName = "Ермолаев",
                FirstName = "Михаил",
                MiddleName = "Сергеевич",
                Email = "Miha_aa@mail.ru",
                UserName = "Mihail"
            };

            var superAdmin = new ApplicationUser
            {
                Id = "c13a33e3-90a7-4dc6-b279-503f878e929d",
                LastName = "Админович",
                FirstName = "CуперАдмин",
                Email = "SuperAdmin@mail.ru",
                UserName = "superAdmin"
            };

            var admin = new ApplicationUser
            {
                Id = "1a676be3-90a7-4dc6-b279-6c051205020b",
                LastName = "Админович",
                FirstName = "Админ",
                Email = "Admin@mail.ru",
                UserName = "Admin"
            };

            var user = new ApplicationUser
            {
                Id = "4dc66be3-90a7-4dc6-b279-6c051205b279",
                LastName = "User",
                FirstName = "User",
                MiddleName = "User",
                Email = "User@mail.ru",
                UserName = "User"
            };
            
            CreateUser(userManager, mihail, superAdminRole.Name);
            CreateUser(userManager, superAdmin, superAdminRole.Name);
            CreateUser(userManager, admin, adminRole.Name);
            CreateUser(userManager, user, userRole.Name);

            #endregion
            #region Disciplines
            db.Disciplines.Add(new Discipline { Id = 1, Name = "Математика", Description = "Математика - самая главная дисциплина среди всех.." });
            db.Disciplines.Add(new Discipline { Id = 2, Name = "Физика", Description = "Физика - самая главная дисциплина среди всех.." });
            db.Disciplines.Add(new Discipline { Id = 3, Name = "Экономика", Description = "Экономика - самая главная дисциплина среди всех.." });
            db.Disciplines.Add(new Discipline { Id = 4, Name = "Химия", Description = "Химия - самая главная дисциплина среди всех.." });
            db.Disciplines.Add(new Discipline { Id = 5, Name = "Программирование", Description = "Программирование - самая главная дисциплина среди всех.." });
            #endregion
            #region Courses
            db.Courses.Add(new de.Course { Id = 1, Name = "Тригонометрия", DisciplineId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Тригонометрия. "+
            "Из основ математических наук, изучаемых в средней школе, как показывает опыт, наиболее поверхностно"+
            "и во многом формально проводится учение о круговых функциях, традиционно называемое тригонометрией."});
            db.Courses.Add(new de.Course { Id = 2, Name = "Высшая математика", DisciplineId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Высшая математика"});
            db.Courses.Add(new de.Course { Id = 3, Name = "Теория вероятности", DisciplineId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Теория вероятности"});
            db.Courses.Add(new de.Course { Id = 4, Name = "Школьная физика", DisciplineId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Школьная физика"});
            db.Courses.Add(new de.Course { Id = 5, Name = "Высшая физика", DisciplineId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Высшая физика"});
            db.Courses.Add(new de.Course { Id = 6, Name = "Теория физики", DisciplineId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Теория физики"});
            db.Courses.Add(new de.Course { Id = 7, Name = "Прикладная экономика", DisciplineId = 3, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Прикладная экономика"});
            db.Courses.Add(new de.Course { Id = 8, Name = "Высшая экономика", DisciplineId = 3, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Высшая экономика"});
            db.Courses.Add(new de.Course { Id = 9, Name = "Теория экономики", DisciplineId = 3, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Теория экономики"});
            db.Courses.Add(new de.Course { Id = 10, Name = "Прикладная химия", DisciplineId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Прикладная химия"});
            db.Courses.Add(new de.Course { Id = 11, Name = "Высшая химия", DisciplineId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Высшая химия"});
            db.Courses.Add(new de.Course { Id = 12, Name = "Теория химии", DisciplineId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Теория химии"});
            db.Courses.Add(new de.Course { Id = 13, Name = "ООП", DisciplineId = 5, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме ООП"});
            db.Courses.Add(new de.Course { Id = 14, Name = "Базы Данных", DisciplineId = 5, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Базы Данных"});
            db.Courses.Add(new de.Course { Id = 15, Name = "Модульное тестирование", DisciplineId = 5, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
            Description = "Этот курс предназначен для тренировки решения задач по теме Модульное тестирование"});
            #endregion
            #region Topics
            db.Topics.Add(new Topic { Id = 1, Name = "Метод вспомогательного аргумента",
                PlannedComplexity = 0.1m,
                CourseId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d", Description = "На уроках алгебры учителя рассказывают, "+
                "что существует небольшой (на самом деле — очень даже большой) класс тригонометрических уравнений, "+
                "которые не решаются стандартными способами — ни через разложение на множители, ни через замену переменной,"+
                "ни даже через однородные слагаемые. В этом случае в дело вступает принципиально другой подход — метод вспомогательного угла."});
            db.Topics.Add(new Topic { Id = 2, Name = "Тригонометрические уравнения",
                PlannedComplexity = 0.3m,
                CourseId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d" ,
                Description = "Некоторое описание Тригонометрические уравнения"});
            db.Topics.Add(new Topic { Id = 3, Name = "Однородные уравнения",
                PlannedComplexity = 0.234m,
                CourseId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Однородные уравнения"});
            db.Topics.Add(new Topic { Id = 4, Name = "Дифференциальные уравнения",
                PlannedComplexity = 0.6m,
                CourseId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Дифференциальные уравнения"});
            db.Topics.Add(new Topic { Id = 5, Name = "Интегралы",
                PlannedComplexity = 0.8m,
                CourseId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Интегралы"});
            db.Topics.Add(new Topic { Id = 6, Name = "Комплексные числа",
                PlannedComplexity = 0.9m,
                CourseId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Комплексные числа"});
            db.Topics.Add(new Topic { Id = 7, Name = "Электромагнетизм",
                PlannedComplexity = 0.7m,
                CourseId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Электромагнетизм"});
            db.Topics.Add(new Topic { Id = 8, Name = "Термодинамика",
                PlannedComplexity = 0.8m,
                CourseId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Термодинамика"});
            db.Topics.Add(new Topic { Id = 9, Name = "Статистическая физика",
                PlannedComplexity = 0.9m,
                CourseId = 4, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Статистическая физика"});
            #endregion
            #region Tasks
            db.Tasks.Add(new de.Task { Id = 1, PlannedComplexity = 7.7m,
                PlannedTime = 300, Answer = "κ*π/5+π/10", Name = "Задача 1.1", Condition= "tg3x=1/tg2x. Чему равен x?",
                PeriodicityOfRequirement = 1.2m, PeriodicityOfVisiting = 4.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 1});
            db.Tasks.Add(new de.Task { Id = 2, PlannedComplexity = 2.57m,
                PlannedTime = 220, Answer = "√2/3", Name = "Задача 1.2", Condition= "Вычислить sin(-585°+α).",
                PeriodicityOfRequirement = 5.1m, PeriodicityOfVisiting = 7.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 2
            });
            db.Tasks.Add(new de.Task { Id = 3, PlannedComplexity = 5.2m,
                PlannedTime = 140, Answer = "nπ+(−1)^n*π6", Name = "Задача 1.3", Condition= "Решить уравнение (cosx)^2+sinx=5/4",
                PeriodicityOfRequirement = 8.1m, PeriodicityOfVisiting = 5.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 4
            });
            db.Tasks.Add(new de.Task { Id = 4, PlannedComplexity = 3.1m,
                PlannedTime = 500, Answer = "arctg3/2 + πn", Name = "Задача 2.1", Condition= "2 sin x – 3 cos x = 0. Чему равен x?",
                PeriodicityOfRequirement = 2.1m, PeriodicityOfVisiting = 3.4m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 3
            });
            db.Tasks.Add(new de.Task { Id = 5, PlannedComplexity = 2.95m,
                PlannedTime = 195, Answer = "√2/3", Name = "Задача 2.2", Condition= "(sinx)^2 – 3*sinx*cos x + 2(cosx)^2 = 0. Чему равен x?",
                PeriodicityOfRequirement = 2.5m, PeriodicityOfVisiting = 9.3m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 5
            });
            db.Tasks.Add(new de.Task { Id = 6, PlannedComplexity = 1.77m,
                PlannedTime = 410, Answer = "-arctg(2)+πk", Name = "Задача 2.3", Condition= "sinx + 2cosx = 0. Чему равен x?",
                PeriodicityOfRequirement = 1.1m, PeriodicityOfVisiting = 0.2m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Weight = 7
            });
            #endregion
            
            db.SaveChanges();
        }

        private void CreateUser(ApplicationUserManager userManager, ApplicationUser user, string roleName)
        {
            string password = "Password";
            var result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}