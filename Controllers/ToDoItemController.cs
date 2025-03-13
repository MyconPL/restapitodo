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
        [Route("{id}")]
        public async Task<ActionResult<List<ToDoItem>>> GetToDo(int id)
        {
            var toDoItems = await _context.ToDoItems.FindAsync(id);
            if (toDoItems is null)
                return NotFound("Nie znaleziono zadania");
            return Ok(toDoItems);
        }
    }
}
