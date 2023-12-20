using ToDoApp.Domain.DTO;

namespace ToDoApp.Domain.Request
{
    public class UpdateTareaRequest
    {
        public int Id { get; set; }

        public string InfoTarea { get; set; }
    }
}
