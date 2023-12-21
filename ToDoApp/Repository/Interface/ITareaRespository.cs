using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Repository.Interface
{
    public interface ITareaRespository
    {
        public Task<List<Tarea>> GetAllActivasAsync();
        public Task<List<Tarea>> GetAllBajasAsync();

        public Task<bool> AddTaskAsync(TareaDTO tarea);

        public Task<bool> UpdateTaskAsync(int id, TareaDTO changes);

        public Task<bool> DeleteTaskAsync(int id);
    }
}
