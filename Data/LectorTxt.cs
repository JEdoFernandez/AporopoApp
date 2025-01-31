using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Models;

namespace TaskManagerAPI.Data
{
    public static class TxtFileHelper
    {
        private static readonly string tasksFilePath = "Data/tasks.txt";
        private static readonly string usersFilePath = "Data/users.txt";

        // Leer todas las tareas desde tasks.txt
        public static List<TareaItem> ReadTasks()
        {
            if (!File.Exists(tasksFilePath)) return new List<TareaItem>();
            
            var lines = File.ReadAllLines(tasksFilePath);
            List<TareaItem> taskList = new List<TareaItem>();
            foreach (string line in lines)
            {
                taskList.Add(LineToTask(line));
            }
            return taskList;
        }

        // Escribir todas las tareas en tasks.txt
        public static void WriteTasks(List<TareaItem> tasks)
        {
            List<string> lines = new List<string>();
            foreach (TareaItem task in tasks)
            {
                lines.Add(TaskToLine(task));
            }
            File.WriteAllLines(tasksFilePath, lines);
        }

        // Convertir una línea de texto en un objeto TaskItem
        private static TareaItem LineToTask(string line)
        {
            var parts = line.Split('|');
            return new TareaItem
            {
                TareaId = int.Parse(parts[0]),
                Titulo = parts[1],
                Descripcion = parts[2],
                Fecha = DateTime.Parse(parts[3]),
                Estado = bool.Parse(parts[4])
            };
        }

        // Convertir un objeto TaskItem en una línea de texto
        private static string TaskToLine(TareaItem task)
        {
            return $"{task.TareaId}|{task.Titulo}|{task.Descripcion}|{task.Fecha}|{task.Estado}";
        }

        // Leer todos los usuarios desde users.txt
        public static List<Usuario> ReadUsers()
        {
            if (!File.Exists(usersFilePath)) return new List<Usuario>();
            
            var lines = File.ReadAllLines(usersFilePath);
            List<Usuario> userList = new List<Usuario>();
            foreach (string line in lines)
            {
                userList.Add(LineToUser(line));
            }
            return userList;
        }

        // Escribir todos los usuarios en users.txt
        public static void WriteUsers(List<Usuario> users)
        {
            List<string> lines = new List<string>();
            foreach (Usuario user in users)
            {
                lines.Add(UserToLine(user));
            }
            File.WriteAllLines(usersFilePath, lines);
        }

        // Convertir una línea de texto en un objeto User
        private static Usuario LineToUser(string line)
        {
            var parts = line.Split('|');
            return new Usuario
            {
                Id = int.Parse(parts[0]),
                NombreUsuario = parts[1],
                Password = parts[2]
            };
        }

        // Convertir un objeto User en una línea de texto
        private static string UserToLine(Usuario user)
        {
            return $"{user.Id}|{user.NombreUsuario}|{user.Password}";
        }
    }
}
