using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_4_OOP; 
using System;

namespace Lab5_Tests
{
    //  новий тест-клас для перевірки логіки методів менеджера
    [TestClass]
    public class DroneManagerTests
    {
        private DroneManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new DroneManager();
        }

        [TestCleanup]
        public void Teardown()
        {
            _manager = null;
        }

        [TestMethod]
        public void AddDroneDirectly_CorrectObject_ShouldAddToListAndAssignId()
        {
            // arrange, act, assert за шаблоном AAA
            Drone drone = new Drone(0, "Test Model", 3000, 50);
            _manager.AddDroneDirectly(drone);

            Assert.AreEqual(1, _manager.GetList().Count);
            Assert.AreEqual(1, _manager.GetList()[0].Id); // перевірка автоматичного id
        }

        [TestMethod]
        public void RemoveDrone_ExistingId_ShouldReturnTrueAndRemoveObject()
        {
            Drone drone = new Drone(0, "To Remove", 2000, 40);
            _manager.AddDroneDirectly(drone);

            bool isRemoved = _manager.RemoveDrone(1);

            Assert.IsTrue(isRemoved);
            Assert.AreEqual(0, _manager.GetList().Count);
        }

        [TestMethod]
        public void GetReadyDronesCount_DifferentStatuses_ShouldReturnCorrectCount()
        {
            Drone d1 = new Drone(0, "Drone 1", 3000, 50); // статус за дефолтом готовий
            Drone d2 = new Drone(0, "Drone 2", 4000, 60);
            d2.Status = "Потрібна зарядка"; // штучно міняю статус

            _manager.AddDroneDirectly(d1);
            _manager.AddDroneDirectly(d2);

            int readyCount = _manager.GetReadyDronesCount();

            Assert.AreEqual(1, readyCount); // готовим має залишитися тільки один
        }

        [TestMethod]
        public void GetFastestDrone_MultipleDrones_ShouldReturnDroneWithMaxSpeed()
        {
            Drone slowDrone = new Drone(0, "Slow", 2000, 40);
            Drone fastDrone = new Drone(0, "Fast", 5000, 150); // максимальна швидкість

            _manager.AddDroneDirectly(slowDrone);
            _manager.AddDroneDirectly(fastDrone);

            Drone result = _manager.GetFastestDrone();

            Assert.IsNotNull(result);
            Assert.AreEqual("Fast", result.Model);
        }

        [TestMethod]
        public void GetFastestDrone_EmptyList_ShouldReturnNull()
        {
            // перевірка поведінки методу на порожньому списку
            Drone result = _manager.GetFastestDrone();
            Assert.IsNull(result);
        }
    }
}
