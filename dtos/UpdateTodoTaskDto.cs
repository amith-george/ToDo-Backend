using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs
{
    public class UpdateTodoTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
