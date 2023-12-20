using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Repository;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TareaService : ITareaService
    {
        private readonly ToDoContext _todoContext;

        public TareaService(ToDoContext context)
        {
            _todoContext = context;
        }
        public async Task<List<Tarea>> GetAllTareasActivasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo)
                                      .ToListAsync();
        }

        public async Task<List<Tarea>> GetAllTareasBajasAsync()
        {
            return await _todoContext.Tareas
                                      .Where(task => task.Activo == false)
                                      .ToListAsync();
        }

        public async Task<bool> AddTareaAsync(TareaDTO tarea)
        {
            var newTask = new Tarea();

            if (tarea.Titulo != null && tarea.Descripcion != null)
            {
                newTask.Titulo = tarea.Titulo;
                newTask.Descripcion = tarea.Descripcion;
                newTask.FechaAlta = DateTime.Now;
                newTask.FechaModificacion = DateTime.Now;
                newTask.Activo = true;

                if (tarea.Estado.Trim() == "pendiente" || tarea.Estado.Trim() == "en curso" || tarea.Estado.Trim() == "finalizada")
                {
                    newTask.Estado = tarea.Estado.ToUpper();
                }
                else
                {
                    return false;
                }

                await _todoContext.Tareas.AddAsync(newTask);
            }

            int rows = await _todoContext.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> UpdateTareaAsync(int id, TareaDTO changes)
        {
            var updatedTask = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id && t.Activo);

            if (updatedTask == null) return false;
           
            if (changes.Estado.ToLower() == "pendiente" || changes.Estado.ToLower() == "en curso" || changes.Estado.ToLower() == "finalizado")
            {
                updatedTask.Estado = changes.Estado.ToUpper();
                updatedTask.Descripcion = changes.Descripcion;
                updatedTask.FechaModificacion = DateTime.Now;
                updatedTask.Titulo = changes.Titulo;

                int rows = await _todoContext.SaveChangesAsync();
                return rows > 0;
            }

            return false;
        }

        public async Task<bool> DeleteTareasAsync(int id)
        {
            var tareaDeleted = await _todoContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
            if (tareaDeleted == null) return false;
            tareaDeleted.Activo = false;

            int rows = await _todoContext.SaveChangesAsync();
        
            return rows > 0; 
        }


    }
}
