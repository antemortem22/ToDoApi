namespace ToDoApp.Repository
{
    public class DBService
    {
        public readonly ToDoContext _todoContext;

        public DBService(ToDoContext context)
        {
            _todoContext = context;
        }
    }
}
