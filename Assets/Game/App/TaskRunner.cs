
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.App
{
    public sealed class TaskRunner 
    {
        private async void RunTasks(IEnumerable<IAsyncTask> tasks)
        {
            foreach (var task in tasks)
            {
                await task.Run();
            }
        }
    }

    public interface IAsyncTask
    {
        //TODO: UniTask here
        Task Run();
    }
}