//using System.Collections.Generic;
//using System.Linq;
//using Course.WEB.Controllers;
//using Course.WEB.Models.Entities;
//using Course.WEB.Models.Interfaces;
//using Course.WEB.Models.Repositories;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace Diplom.Tests
//{
//    [TestClass]
//    public class TaskControllerTest
//    {
//        [TestMethod]
//        public void CanCreateTask()
//        {
//            Task task = new Task
//            {
//                Id = 1,
//                PlannedComplexity = 7.7m,
//                PlannedTime = 300,
//                Answer = "κ*π/5+π/10",
//                Name = "Задача 1.1",
//                Condition = "tg3x=1/tg2x. Чему равен x?",
//                PeriodicityOfRequirement = 1.2m,
//                PeriodicityOfVisiting = 4.1m,
//                TopicId = 1,
//                CreatorId = "65c13a33-5a3e-450c-bd46-503f878e929d",
//                Weight = 1
//            };
//            var taskMock = new Mock<IRepository<Task>>();
//            taskMock.Setup(x => x.GetAll()).Returns(new List<Task> {task});
//            var uof = new Mock<EFUnitOfWork>();
//            uof.Setup(x => x.Tasks).Returns(taskMock.Object);
//            TaskController controller = new TaskController
//            {
//                db = uof.Object
//            };

//            controller.Create(task);

//            Assert.AreEqual(controller.db.Tasks.GetAll().Count(), 1);
//        }

//        [TestMethod]
//        public void CanGetTask()
//        {
//            Task task = new Task
//            {
//                Id = 1,
//                PlannedComplexity = 7.7m,
//                PlannedTime = 300,
//                Answer = "κ*π/5+π/10",
//                Name = "Задача 1.1",
//                Condition = "tg3x=1/tg2x. Чему равен x?",
//                PeriodicityOfRequirement = 1.2m,
//                PeriodicityOfVisiting = 4.1m,
//                TopicId = 1,
//                CreatorId = "65c13a33-5a3e-450c-bd46-503f878e929d",
//                Weight = 1
//            };
//            var taskMock = new Mock<IRepository<Task>>();
//            taskMock.Setup(x => x.Get(1)).Returns(task);
//            var uof = new Mock<EFUnitOfWork>();
//            uof.Setup(x => x.Tasks).Returns(taskMock.Object);
//            TaskController controller = new TaskController
//            {
//                db = uof.Object
//            };

//           var result = controller.Edit(1);

//            Assert.AreEqual(result, task);
//        }

//        [TestMethod]
//        public void CanDeleteTask()
//        {
//            var taskMock = new Mock<IRepository<Task>>();
//            taskMock.Setup(x => x.Delete(1));
//            taskMock.Setup(x => x.GetAll()).Returns(new List<Task> ());
//            var uof = new Mock<EFUnitOfWork>();
//            uof.Setup(x => x.Tasks).Returns(taskMock.Object);
//            TaskController controller = new TaskController
//            {
//                db = uof.Object
//            };

//            controller.Delete(1);

//            Assert.AreEqual(controller.db.Tasks.GetAll().Count(), 0);
//        }
//    }
//}
