namespace ToDoApp.Domain.DTO
{
    public class TareaDTO
    {
        public string Titulo { get; set; }

        public string Estado { get; set; } = "pendiente";

        public string Descripcion { get; set; }
    }
}
