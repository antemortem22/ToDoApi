using Microsoft.AspNetCore.Mvc;
using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Request;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private ITareaService _tareasService;
        public TareasController(ITareaService tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet("Lista de tareas activas")]

        public async Task<IActionResult> GetTareasActivas()
        {
            var result = await _tareasService.GetAllTareasActivasAsync();

            return Ok(result);

        }

        [HttpGet("Lista de tareas dadas de baja")]
        public async Task<IActionResult> GetTareasBajas()
        {
            var result = await _tareasService.GetAllTareasBajasAsync();

            return Ok(result);

        }

        [HttpPost("Agregar tareas")]
        public async Task<IActionResult> AddTarea([FromBody] TareaDTO request)
        {
            var result = await _tareasService.AddTareaAsync(request);

            if (!result) return BadRequest(new { Error = "Se rechazo la tarea" });
            return Created("", new { Message = "La tarea fue creada correctamente" });
        }

        [HttpPut("Updatear tareas")]

        public async Task<IActionResult> UpdateTarea([FromBody] UpdateTareaRequest request)
        {
            var result = await _tareasService.UpdateTareaAsync(request.Id, request.InfoTarea);

            if (!result) return BadRequest(new { Error = "No se pudo modificar la tarea" });

            return Ok(new { Message = "La tarea se modifico correctamente" });
        }

        [HttpDelete("Borrado logico de tareas")]
        public async Task<IActionResult> DeleteTareasAsync(int id)
        {
            var result = await _tareasService.DeleteTareasAsync(id);
            if (!result) return BadRequest(new { Error = "No se pudo borrar la tarea" });

            return Ok(new { Message = "Se borro la tarea correctamente uwu" });
        }


    }




}
