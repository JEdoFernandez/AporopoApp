using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Data;
using Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TareaItem>> GetTasks()
        {
            var tasks = TxtFileHelper.ReadTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TareaItem> GetTask(int id)
        {
            var tasks = TxtFileHelper.ReadTasks();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TareaItem> CreateTask(TareaItem task)
        {
            var tasks = TxtFileHelper.ReadTasks();
            task.TareaId = tasks.Count > 0 ? tasks.Max(t => t.TareaId) + 1 : 1;
            tasks.Add(task);
            TxtFileHelper.WriteTasks(tasks);
            return CreatedAtAction(nameof(GetTask), new { id = task.TareaId }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TareaItem updatedTask)
        {
            var tasks = TxtFileHelper.ReadTasks();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();

            task.Titulo = updatedTask.Titulo;
            task.Descripcion = updatedTask.Descripcion;
            task.Fecha = updatedTask.Fecha;
            task.Estado = updatedTask.Estado;
            
            TxtFileHelper.WriteTasks(tasks);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var tasks = TxtFileHelper.ReadTasks();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            TxtFileHelper.WriteTasks(tasks);
            return NoContent();
        }
    }
}
