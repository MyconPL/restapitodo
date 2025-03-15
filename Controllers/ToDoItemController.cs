using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restapitodo.Data;
using restapitodo.Models;

namespace restapitodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly DataContext _context;

        public ToDoItemController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<List<ToDoItem>>> GetAllToDoItems()
        {
            var toDoItems = await _context.ToDoItems.ToListAsync();
            return Ok(toDoItems); 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<ToDoItem>>> GetToDo(int id)
        {
            var toDoItems = await _context.ToDoItems.FindAsync(id);
            if (toDoItems is null)
                return NotFound("Nie znaleziono zadania");
            return Ok(toDoItems);
        }
        [HttpPost]
        public async Task<ActionResult<List<ToDoItem>>> AddToDo([FromBody] ToDoItem newTodo)
        {
            if (newTodo == null || string.IsNullOrWhiteSpace(newTodo.Title))
                return BadRequest("Nie podano zadania");

            _context.ToDoItems.Add(newTodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDo), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<List<ToDoItem>>> ToggleCompletion(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem is null)
                return NotFound("Nie znaleziono zadania");

            toDoItem.IsComplete = !toDoItem.IsComplete;
            await _context.SaveChangesAsync();
            return Ok(toDoItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<ToDoItem>>> DeleteToDo(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem is null)
                return NotFound("Nie znaleziono zadania");

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return Ok(toDoItem);
        }
    }
}