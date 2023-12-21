using ToDoApp.Domain.DTO;
using ToDoApp.Domain.Entities;
using ToDoApp.Repository.Interface;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services
{
    public class TareaService : ITareaService
    {
        ITareaRespository _repository;

        public TareaService(ITareaRespository repository)
        {
            _repository = repository;
        }

        public async Task<List<Tarea>> GetAllTareasActivasAsync()
        {
            var result = await _repository.GetAllActivasAsync();

            return result;
        }

        public async Task<List<Tarea>> GetAllTareasBajasAsync()
        {
            var result = await _repository.GetAllBajasAsync();

            return result;
        }

        public async Task<bool> AddTareaAsync(TareaDTO tarea)
        {
            var result = await _repository.AddTaskAsync(tarea);

            return result;
        }

        public async Task<bool> UpdateTareaAsync(int id, TareaDTO changes)
        {
            var result = await _repository.UpdateTaskAsync(id, changes);

            return result;

        }

        public async Task<bool> DeleteTareasAsync(int id)
        {
            var result = await _repository.DeleteTaskAsync(id);

            return result;
        }
    }

    //Validaciones 

    
}
