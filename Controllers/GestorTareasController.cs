using Microsoft.AspNetCore.Mvc;
using AporopoApi.Data;
using Models;

namespace GestorTareasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<TareaItem>> GetTasks()
        {
            var tasks = TxtProcesador.LeerTareas();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TareaItem> GetTask(int id)
        {
            var tasks = TxtProcesador.LeerTareas();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<TareaItem> CreateTask(TareaItem task)
        {
            var tasks = TxtProcesador.LeerTareas();
            task.TareaId = tasks.Count > 0 ? tasks.Max(t => t.TareaId) + 1 : 1;
            tasks.Add(task);
            TxtProcesador.CrearTareas(tasks);
            return CreatedAtAction(nameof(GetTask), new { id = task.TareaId }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TareaItem updatedTask)
        {
            var tasks = TxtProcesador.LeerTareas();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();

            task.Titulo = updatedTask.Titulo;
            task.Descripcion = updatedTask.Descripcion;
            task.Fecha = updatedTask.Fecha;
            task.Estado = updatedTask.Estado;
            
            TxtProcesador.CrearTareas(tasks);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var tasks = TxtProcesador.LeerTareas();
            var task = tasks.FirstOrDefault(t => t.TareaId == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            TxtProcesador.CrearTareas(tasks);
            return NoContent();
        }
    }
}
