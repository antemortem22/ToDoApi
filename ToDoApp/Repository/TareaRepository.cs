using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Repository.Interface;

namespace ToDoApp.Repository
{
    public class TareaRepository : ITareaRespository
    {
        private readonly ToDoContext _todoContext;

        public TareaRepository(ToDoContext context)
        {
            _todoContext = context;
        }

        public async Task<List<Tarea>> GetAllActivasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo)
                                      .ToListAsync();
        }
        public async Task<List<Tarea>> GetAllBajasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo == false)
                                      .ToListAsync();
        }

        public async Task<bool> AddTaskAsync(TareaDTO tarea)
        {
            if (TaskValidator.TareaEsInvalida(tarea) || TaskValidator.EsNullOEspacio(tarea))
            {
                return false;
            }

            var newTask = new Tarea();

            newTask.Titulo = tarea.Titulo;
            newTask.Descripcion = tarea.Descripcion;
            newTask.FechaAlta = DateTime.Now;
            newTask.FechaModificacion = DateTime.Now;
            newTask.Activo = true;

            if (TaskValidator.EsEstadoInvalido(tarea.Estado))
            {
                return false;
            }

            newTask.Estado = tarea.Estado.ToUpper();

            await _todoContext.Tareas.AddAsync(newTask);

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> UpdateTaskAsync(int id, TareaDTO changes)
        {
            var updatedTask = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id && t.Activo);

            if (updatedTask == null || TaskValidator.EsEstadoInvalido(changes.Estado))
            {
                return false;
            }

            updatedTask.Estado = changes.Estado.ToUpper();
            updatedTask.Descripcion = changes.Descripcion;
            updatedTask.FechaModificacion = DateTime.Now;
            updatedTask.Titulo = changes.Titulo;

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var tareaDeleted = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
            if (tareaDeleted == null) return false;
            tareaDeleted.Activo = false;

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public static class TaskValidator
        {
            public static bool EsEstadoInvalido(string estado)
            {
                string estadoNormalizado = estado?.ToLower().Trim();
                return estadoNormalizado != "pendiente" && estadoNormalizado != "en curso" && estadoNormalizado != "finalizada";
            }

            public static bool TareaEsInvalida(TareaDTO tarea)
            {
                return tarea.Titulo == null || tarea.Descripcion == null;
            }

            public static bool EsNullOEspacio(TareaDTO tarea)
            {
                bool validation = string.IsNullOrWhiteSpace(tarea.Titulo) || string.IsNullOrWhiteSpace(tarea.Descripcion);
                return validation;
            }
        }

    }

}
