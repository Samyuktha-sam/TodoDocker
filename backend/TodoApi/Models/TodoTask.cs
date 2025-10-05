using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models;

[Table("Task")]
public class TodoTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title length can't be more than 100 characters.")]
    [Column(TypeName = "varchar(100)")]
    public string Title { get; set; }

    [StringLength(600, ErrorMessage = "Description length can't be more than 600 characters.")]
    [Column(TypeName = "varchar(600)")]
    public string Description { get; set; }

    public bool IsCompleted { get; set; } = false;
    
    [Column(TypeName = "timestamp with time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Priority Priority { get; set; } = Priority.Low;
}

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2
}