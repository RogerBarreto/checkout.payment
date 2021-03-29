using clean_architecture.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace clean_architecture.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
