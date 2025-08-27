using System.ComponentModel.DataAnnotations;

namespace TodoWebAppEF.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public bool IsDone { get; set; } = false;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
