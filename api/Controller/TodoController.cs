using System.Data.Entity;
using api.Context;
using api.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoContext _db;

    public TodoController(TodoContext todoContext)
    {
        _db = todoContext;
    }
    
    [HttpGet("/todo")]
    public List<TodoItem> GetList()
    {
        return _db.TodoItems.ToList();
    }
    
    [HttpPost("/todo")]
    public List<TodoItem> PostList([FromBody] TodoItem todoItem)
    {
        _db.TodoItems.Add(todoItem);
        _db.SaveChanges();
        return _db.TodoItems.ToList();
    }   
}