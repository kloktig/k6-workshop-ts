using api.Context;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoContext _db;
    private readonly ILogger<TodoController> _logger;

    public TodoController(TodoContext todoContext, ILogger<TodoController> logger)
    {
        _db = todoContext;
        _logger = logger;
    }
    
    [HttpGet("/todo")]
    public List<TodoItem> GetList()
    {
        _logger.LogInformation("Getting list");
        return _db.TodoItems.ToList();
    }
    
    [HttpPost("/todo")]
    public List<TodoItem> PostList([FromBody] TodoItemDto todoItem)
    {
        _db.TodoItems.Add(new TodoItem {ToDoTask = todoItem.ToDoTask});
        _db.SaveChanges();
        return _db.TodoItems.ToList();
    }   
}