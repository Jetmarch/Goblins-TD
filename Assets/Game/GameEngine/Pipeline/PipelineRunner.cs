using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.GameEngine.Pipeline
{
    public sealed class PipelineRunner
    {
        public async UniTask Execute(Pipeline pipeline)
        {
            foreach (var task in pipeline.Tasks)
            {
                var timeDelta = Time.deltaTime;
                await task.Run(timeDelta);
            }
        }
    }

    public sealed class Pipeline
    {
        public IReadOnlyCollection<IPipelineTask> Tasks => _tasks;
        private readonly List<IPipelineTask> _tasks = new();

        public void AddTask(IPipelineTask task) => _tasks.Add(task);
    }

    public interface IPipelineTask
    {
        UniTask Run(float deltaTime);
    }
}