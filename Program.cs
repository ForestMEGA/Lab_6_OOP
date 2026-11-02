using System;
using System.Collections.Generic;

namespace Lab_4_OOP
{
    class Program
    {
        private static DroneManager manager = new DroneManager();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            manager.AddDroneDirectly(new Drone(manager.GetNextId(), "DJI Mavic 3", 5000, 70));
            manager.AddDroneDirectly(new Drone(manager.GetNextId(), "FPV Bomber", 4500, 130));

            while (true)
            {
                Console.WriteLine("\n МЕНЮ КЕРУВАННЯ ДРОНАМИ ");
                Console.WriteLine("1 - Додати об'єкт");
                Console.WriteLine("2 - Переглянути всі об'єкти");
                Console.WriteLine("3 - Знайти об'єкт");
                Console.WriteLine("4 - Продемонструвати поведінку");
                Console.WriteLine("5 - Видалити об'єкт");
                Console.WriteLine("6 - Показати аналітику списку");
                Console.WriteLine("0 - Вийти з програми");
                Console.WriteLine("Виберіть дію:");

                string choice = Console.ReadLine()?.Trim();

                if (choice == "1") CreateDroneMenu();
                else if (choice == "2") ShowAllMenu();
                else if (choice == "3") FindMenu();
                else if (choice == "4") FlightMenu();
                else if (choice == "5") DeleteMenu();
                else if (choice == "6") AnalyticsMenu();
                else if (choice == "0") { Console.WriteLine("Вихід з програми..."); break; }
                else Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
            }
        }

        static void CreateDroneMenu()
        {
            try
            {
                Console.WriteLine("\nВведіть дані дрона (Формат: Модель;Батарея;Швидкість):");
                string inputRow = Console.ReadLine();

                if (Drone.TryParse(inputRow, out Drone parsedDrone))
                {
                    manager.AddDroneDirectly(parsedDrone);
                    Console.WriteLine("[УСПІХ]: Дрон успішно додано.");
                }
                else
                {
                    Console.WriteLine("[ПОМИЛКА]: Некоректний формат рядка.");
                }
            }
            catch (Exception ex) { Console.WriteLine($"[ПОМИЛКА]: {ex.Message}"); }
        }

        static void ShowAllMenu()
        {
            Console.WriteLine($"\nВсього створено об'єктів у класі: {Drone.CorrectlyCreatedObjectsCount}");
            List<Drone> all = manager.GetList();
            if (all.Count == 0) { Console.WriteLine("Список порожній."); return; }
            for (int i = 0; i < all.Count; i++) Console.WriteLine(all[i].GetDetails());
        }

        static void FindMenu()
        {
            Console.WriteLine("Введіть ID дрона для пошуку:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Drone found = manager.FindById(id);
                if (found != null) Console.WriteLine(found.GetDetails());
                else Console.WriteLine("Дрон не знайдено.");
            }
        }

        static void FlightMenu()
        {
            try
            {
                Console.WriteLine("Введіть ID дрона:");
                int id = Convert.ToInt32(Console.ReadLine());
                Drone activeDrone = manager.FindById(id);
                if (activeDrone == null) { Console.WriteLine("Дрон не знайдений!"); return; }

                Console.WriteLine("Введіть дистанцію польоту (км):");
                double dist = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(activeDrone.StartFlight(dist));
            }
            catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
        }

        static void DeleteMenu()
        {
            Console.WriteLine("Введіть ID дрона для видалення:");
            if (int.TryParse(Console.ReadLine(), out int id) && manager.RemoveDrone(id))
                Console.WriteLine("Об'єкт видалено.");
            else
                Console.WriteLine("Дрон не знайдено.");
        }

        // виведення результатів чистих методів менеджера
        static void AnalyticsMenu()
        {
            Console.WriteLine("\n АНАЛІТИКА КОЛЕКЦІЇ ");
            Console.WriteLine($"Кількість дронів, готових до вильоту: {manager.GetReadyDronesCount()}");

            Drone fastest = manager.GetFastestDrone();
            if (fastest != null)
                Console.WriteLine($"Найшвидший дрон у системі: {fastest.Model} ({fastest.MaxSpeed} км/год)");
            else
                Console.WriteLine("У системі немає дронів.");
        }
    }
}
