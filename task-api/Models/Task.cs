using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public bool Completed { get; set; }

        public Task() { }
    }
}