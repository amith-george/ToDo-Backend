using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs
{
    public class CreateTodoTaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
