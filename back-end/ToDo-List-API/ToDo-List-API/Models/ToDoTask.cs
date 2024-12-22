namespace ToDo_List_API.Models
{
    public class ToDoTask
    {
        public int ToDoTaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool isCompleted { get; set; } = false;
        
    }
}
