using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Services.Interfaces
{
    public interface ITareaService
    {
        public Task<List<Tarea>> GetAllTareasActivasAsync();
        public Task<List<Tarea>> GetAllTareasBajasAsync();
  
        public Task<bool> AddTareaAsync(TareaDTO tarea);

        public Task<bool> UpdateTareaAsync(int id, string changes);

        public Task<bool> DeleteTareasAsync(int id);
    }
}
