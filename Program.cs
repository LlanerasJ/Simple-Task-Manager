using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simple_Task_Manager
{
    public static class GlobalObjects
    {
        public static List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        
        private static int _nextTaskId = 1;

        public static int GetNextTaskId()
        {
            return _nextTaskId++;
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Simple Task Manager");

            bool running = true;

            while(running)
            {
                running = RunTaskManager();
            }
        }

        // Adds a new Task to the list
        public static bool RunTaskManager()
        {
            Console.WriteLine("1: Add Task");
            Console.WriteLine("2: View Task");
            Console.WriteLine("3: Mark Tasks Completed");
            Console.WriteLine("4: Delete Tasks");
            Console.WriteLine("5: Save Tasks");
            Console.WriteLine("6: Load Tasks");
            Console.WriteLine("0: Exit");
            string result = Console.ReadLine();

            int choice = int.Parse(result);
            if (choice == 1)
            {
                Console.WriteLine("Please enter a description for the task.");
                string descResult = Console.ReadLine();

                Console.WriteLine("Please enter a due date for the task in this format (1/1/2000). If not needed just press enter.");
                string dueDateResult = Console.ReadLine();

                int id = GlobalObjects.GetNextTaskId();

                TaskItem newTask = new TaskItem
                {
                    Id = id,
                    Description = descResult,
                    DueDate = dueDateResult,
                    IsCompleted = false
                };

                GlobalObjects.Tasks.Add(newTask);
                Console.WriteLine("Task added!");
            }
            else if (choice == 2)
            {
                DisplayTasks();
            }
            else if (choice == 3)
            {
                MarkTasks();
            }
            else if (choice == 4)
            {
                DeleteTasks();
            }
            else if (choice == 5)
            {
                SaveTasks();
            }
            else if (choice == 6)
            {
                LoadTasks();
            }
            else if (choice == 0)
            {
                return false;
            }

            return true;
        }

        // Displays information about all tasks in the list
        public static void DisplayTasks()
        {
            if (GlobalObjects.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\n--- Current Tasks ---");
            foreach (var task in GlobalObjects.Tasks)
            {
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate}");
                Console.WriteLine($"Completed: {(task.IsCompleted ? "Yes" : "No")}");
                Console.WriteLine("-------------------------");
            }
        }

        // Marks tasks completed in list
        public static void MarkTasks()
        {
            

            if (GlobalObjects.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\n--- Current Tasks ---");
            foreach (var task in GlobalObjects.Tasks)
            {
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate}");
                Console.WriteLine($"Completed: {(task.IsCompleted ? "Yes" : "No")}");
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine("Enter in the task Id for the task you would like to mark complete and press enter. To exit type 0 and enter.");

            bool running = true;

            while (running)
            {
                string result = Console.ReadLine();
                int choice = int.Parse(result);

                if (choice !=  0) {
                    var item = GlobalObjects.Tasks.FirstOrDefault(i => i.Id == choice);
                    item.IsCompleted = true;
                    Console.WriteLine("Item marked completed.");
                }
                else
                {
                    running = false;
                }
            }
        }

        // Deletes tasks from list
        public static void DeleteTasks()
        {
            if (GlobalObjects.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\n--- Current Tasks ---");
            foreach (var task in GlobalObjects.Tasks)
            {
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Due Date: {task.DueDate}");
                Console.WriteLine($"Completed: {(task.IsCompleted ? "Yes" : "No")}");
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine("Enter in the task Id for the task you would like to delete and press enter. To exit type 0 and enter.");

            bool running = true;

            while (running)
            {
                string result = Console.ReadLine();
                int choice = int.Parse(result);

                if (choice != 0)
                {
                    var item = GlobalObjects.Tasks.FirstOrDefault(i => i.Id == choice);
                    GlobalObjects.Tasks.Remove(item);
                    Console.WriteLine("Task deleted.");
                }
                else
                {
                    running = false;
                }
            }
        }

        // Saves list and tasks details to a text file
        public static void SaveTasks()
        {
            if (GlobalObjects.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            string filePath = @"C:\Temp\tasks.txt";

            try
            {
                var lines = GlobalObjects.Tasks.Select(task =>
                    $"{task.Id}|{task.Description}|{task.DueDate}|{task.IsCompleted}");

                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"Results saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Loads all tasks from a text file into the list
        public static void LoadTasks()
        {
            Console.WriteLine("Enter in path for text file to load.");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                GlobalObjects.Tasks = lines.Select(line =>
                {
                    // Checks for each value
                    var parts = line.Split('|');
                    return new TaskItem
                    {
                        Id = int.Parse(parts[0]),
                        Description = parts[1],
                        DueDate = parts[2],
                        IsCompleted = bool.Parse(parts[3])
                    };
                }).ToList();

                // Update next ID so it's always unique
                if (GlobalObjects.Tasks.Any())
                {
                    var maxId = GlobalObjects.Tasks.Max(t => t.Id);
                    typeof(GlobalObjects)
                        .GetField("_nextTaskId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        .SetValue(null, maxId + 1);
                }
            }

        }
    }
}
