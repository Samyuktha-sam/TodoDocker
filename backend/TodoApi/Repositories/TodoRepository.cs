using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public TodoTask CreateTodoTask(TodoTask todoTask)
    {
        try
        {
            if (_context.Task.Any(t => t.Title == todoTask.Title))
            {
                throw new Exception("Title already exists");
            }
            else
            {
                _context.Task.Add(todoTask);
                _context.SaveChanges();
                return todoTask;
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public List<TodoTask> GetTodoTasks()
    {
        try
        {
            if (!_context.Task.Any())
            {
                throw new Exception("No tasks found");
            }
            return _context.Task.OrderBy(t => t.IsCompleted)
                .ThenByDescending(t => t.Priority)
                .ThenBy(t => t.CreatedAt)
                .ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public TodoTask GetTodoTask(int id)
    {
        TodoTask? todoTask = _context.Task.Find(id);
        if (todoTask == null)
        {
            throw new Exception("Task not found");
        }

        return todoTask;
    }

    public List<TodoTask> GetRecentFiveTasks()
    {
        try
        {
            if (!_context.Task.Any())
            {
                throw new Exception("No tasks found");
            }
            return _context.Task.OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToList();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public TodoTask UpdateTodoTask(int id, TodoTask todoTask)
    {
        TodoTask? task = _context.Task.Find(id);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        task.Title = todoTask.Title;
        task.Description = todoTask.Description;
        task.IsCompleted = todoTask.IsCompleted;
        task.Priority = todoTask.Priority;
        _context.SaveChanges();
        return task;
    }

    public void DeleteTodoTask(int id)
    {
        TodoTask? task = _context.Task.Find(id);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        try
        {
            _context.Task.Remove(task);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}