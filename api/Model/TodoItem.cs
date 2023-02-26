using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    public string ToDoTask { get; set; }
}

public class TodoItemDto
{
    public string ToDoTask { get; set; }
}