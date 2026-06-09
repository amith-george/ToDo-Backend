using System;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    }
}
