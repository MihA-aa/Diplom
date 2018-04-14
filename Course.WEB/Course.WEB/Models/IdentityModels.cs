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
            : base("CourseDBNew", throwIfV1Schema: false)
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
            #region Create role
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            var role0 = new IdentityRole { Name = "superAdmin" };
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            
            roleManager.Create(role0);
            roleManager.Create(role1);
            roleManager.Create(role2);
            
            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);
            
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
            #endregion
            #region Users
            db.Users.Add(new ApplicationUser
            {
                Id = "65c13a33-5a3e-450c-bd46-503f878e929d",
                LastName = "Ермолаев",
                FirstName = "Михаил",
                MiddleName = "Сергеевич",
                Email = "AMihA_aa@mail.ru",
                PasswordHash = "Password",
                UserName = "Михаил"
            });
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
            db.Topics.Add(new Topic { Id = 1, Name = "Метод вспомогательного аргумента", AverageComplexity = 6.1m,
                CourseId = 1, PeriodicityOfDemand = 2.1m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d", Description = "На уроках алгебры учителя рассказывают, "+
                "что существует небольшой (на самом деле — очень даже большой) класс тригонометрических уравнений, "+
                "которые не решаются стандартными способами — ни через разложение на множители, ни через замену переменной,"+
                "ни даже через однородные слагаемые. В этом случае в дело вступает принципиально другой подход — метод вспомогательного угла."});
            db.Topics.Add(new Topic { Id = 2, Name = "Тригонометрические уравнения", AverageComplexity = 8.3m,
                CourseId = 1, PeriodicityOfDemand = 1.8m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d" ,
                Description = "Некоторое описание Тригонометрические уравнения"});
            db.Topics.Add(new Topic { Id = 3, Name = "Однородные уравнения", AverageComplexity = 7.234m,
                CourseId = 1, PeriodicityOfDemand = 2.0m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Однородные уравнения"});
            db.Topics.Add(new Topic { Id = 4, Name = "Дифференциальные уравнения", AverageComplexity = 6.1m,
                CourseId = 2, PeriodicityOfDemand = 6.5m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Дифференциальные уравнения"});
            db.Topics.Add(new Topic { Id = 5, Name = "Интегралы", AverageComplexity = 8.3m,
                CourseId = 2, PeriodicityOfDemand = 3.8m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Интегралы"});
            db.Topics.Add(new Topic { Id = 6, Name = "Комплексные числа", AverageComplexity = 9.1m,
                CourseId = 2, PeriodicityOfDemand = 3.1m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Комплексные числа"});
            db.Topics.Add(new Topic { Id = 7, Name = "Электромагнетизм", AverageComplexity = 6.1m,
                CourseId = 4, PeriodicityOfDemand = 6.5m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Электромагнетизм"});
            db.Topics.Add(new Topic { Id = 8, Name = "Термодинамика", AverageComplexity = 8.3m,
                CourseId = 4, PeriodicityOfDemand = 3.8m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Термодинамика"});
            db.Topics.Add(new Topic { Id = 9, Name = "Статистическая физика", AverageComplexity = 9.1m,
                CourseId = 4, PeriodicityOfDemand = 3.1m, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d",
                Description = "Некоторое описание Статистическая физика"});
            #endregion
            #region Tasks
            db.Tasks.Add(new Entities.Task { Id = 1, PlannedComplexity = 7.7m, AverageComplexity = 9.1m,
                PlannedTime = 300, AverageTime = 243, Answer = "κ*π/5+π/10", Name = "Задача 1.1", Condition= "tg3x=1/tg2x. Чему равен x?",
                PeriodicityOfRequirement = 1.2m, PeriodicityOfVisiting = 4.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            db.Tasks.Add(new Entities.Task { Id = 2, PlannedComplexity = 2.57m, AverageComplexity = 2.1m,
                PlannedTime = 220, AverageTime = 132, Answer = "√2/3", Name = "Задача 1.2", Condition= "Вычислить sin(-585°+α).",
                PeriodicityOfRequirement = 5.1m, PeriodicityOfVisiting = 7.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            db.Tasks.Add(new Entities.Task { Id = 3, PlannedComplexity = 5.2m, AverageComplexity = 2.4m,
                PlannedTime = 140, AverageTime = 143, Answer = "nπ+(−1)^n*π6", Name = "Задача 1.3", Condition= "Решить уравнение (cosx)^2+sinx=5/4",
                PeriodicityOfRequirement = 8.1m, PeriodicityOfVisiting = 5.1m, TopicId = 1, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            db.Tasks.Add(new Entities.Task { Id = 4, PlannedComplexity = 3.1m, AverageComplexity = 8.4m,
                PlannedTime = 500, AverageTime = 243, Answer = "arctg3/2 + πn", Name = "Задача 2.1", Condition= "2 sin x – 3 cos x = 0. Чему равен x?",
                PeriodicityOfRequirement = 2.1m, PeriodicityOfVisiting = 3.4m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            db.Tasks.Add(new Entities.Task { Id = 5, PlannedComplexity = 2.95m, AverageComplexity = 5.6m,
                PlannedTime = 195, AverageTime = 132, Answer = "√2/3", Name = "Задача 2.2", Condition= "(sinx)^2 – 3*sinx*cos x + 2(cosx)^2 = 0. Чему равен x?",
                PeriodicityOfRequirement = 2.5m, PeriodicityOfVisiting = 9.3m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            db.Tasks.Add(new Entities.Task { Id = 6, PlannedComplexity = 1.77m, AverageComplexity = 4.3m,
                PlannedTime = 410, AverageTime = 143, Answer = "-arctg(2)+πk", Name = "Задача 2.3", Condition= "sinx + 2cosx = 0. Чему равен x?",
                PeriodicityOfRequirement = 1.1m, PeriodicityOfVisiting = 0.2m, TopicId = 2, CreatorId= "65c13a33-5a3e-450c-bd46-503f878e929d"});
            #endregion

            db.SaveChanges();
        }
    }
}