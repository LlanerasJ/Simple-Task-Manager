using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static bool RunTaskManager()
        {
            Console.WriteLine("1: Add Task");
            Console.WriteLine("2: View Task");
            Console.WriteLine("3: Mark Tasks Completed");
            Console.WriteLine("4: Delete Tasks");
            Console.WriteLine("5: Load Tasks");
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

            }
            else if (choice == 4)
            {

            }
            else if (choice == 5)
            {

            }
            else if (choice == 0)
            {
                return false;
            }

            return true;
        }

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
        }

    }
}
