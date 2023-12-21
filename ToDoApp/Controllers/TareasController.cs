using Microsoft.AspNetCore.Mvc;
using ToDoApp.Domain.DTO;
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

        [HttpGet("activeListTask")]

        public async Task<IActionResult> GetTareasActivas()
        {
            var result = await _tareasService.GetAllTareasActivasAsync();

            return Ok(result);

        }

        [HttpGet("inactiveListTask")]
        public async Task<IActionResult> GetTareasBajas()
        {
            var result = await _tareasService.GetAllTareasBajasAsync();

            return Ok(result);

        }

        [HttpPost("addTask")]
        public async Task<IActionResult> AddTarea([FromBody] TareaDTO request)
        {
            var result = await _tareasService.AddTareaAsync(request);

            if (!result) return BadRequest(new { Error = "Se rechazo la tarea" });
            return Created("", new { Message = "La tarea fue creada correctamente" });
        }

        [HttpPut("{id}/updateTask")]

        public async Task<IActionResult> UpdateTarea( int id, [FromBody] TareaDTO request)
        {
            var result = await _tareasService.UpdateTareaAsync(id, request);

            if (!result) return BadRequest(new { Error = "No se pudo modificar la tarea" });

            return Ok(new { Message = "La tarea se modifico correctamente" });
        }

        [HttpDelete("{id}/taskEraser")]
        public async Task<IActionResult> DeleteTareasAsync(int id)
        {
            var result = await _tareasService.DeleteTareasAsync(id);
            if (!result) return BadRequest(new { Error = "No se pudo borrar la tarea" });

            return Ok(new { Message = "Se borro la tarea correctamente uwu" });
        }


    }




}
