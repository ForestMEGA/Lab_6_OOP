using System;
using System.Collections.Generic;

namespace Lab_4_OOP
{
    public class DroneManager
    {
       // всі об'єкти зберігаються у колекції List<T>
        private List<Drone> droneList = new List<Drone>();
        private int currentId = 1;

        public int GetNextId()
        {
            return currentId;
        }

        //  додавання через метод колекції
        public void AddDroneDirectly(Drone drone)
        {
            if (drone.Id <= 0)
            {
                drone.SetAssignedId(currentId);
                currentId++;
            }
            droneList.Add(drone);
        }

        public List<Drone> GetList()
        {
            return droneList;
        }

        //  пошук через методи колекції 
        public Drone FindById(int id)
        {
            for (int i = 0; i < droneList.Count; i++)
            {
                if (droneList[i].Id == id) return droneList[i];
            }
            return null;
        }

        //  видалення через метод колекції
        public bool RemoveDrone(int id)
        {
            Drone target = FindById(id);
            if (target != null)
            {
                return droneList.Remove(target);
            }
            return false;
        }

        // методи, які не залежать від введення/виведення даних (Console)
        public int GetReadyDronesCount()
        {
            int count = 0;
            for (int i = 0; i < droneList.Count; i++)
            {
                if (droneList[i].Status == "Готовий до вильоту")
                {
                    count++;
                }
            }
            return count;
        }

        // метод шукає найшвидший дрон у колекції та повертає його
        public Drone GetFastestDrone()
        {
            if (droneList.Count == 0) return null;

            Drone fastest = droneList[0];
            for (int i = 1; i < droneList.Count; i++)
            {
                if (droneList[i].MaxSpeed > fastest.MaxSpeed)
                {
                    fastest = droneList[i];
                }
            }
            return fastest;
        }
    }
}
