
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Towers;
using Game.Gameplay.Towers.Components;
using Game.Gameplay.Towers.Presenters;
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

    public sealed class RotateToTargetTask : IPipelineTask
    {
        private readonly TowerData _towerData;
        private readonly ITowerView _view;
        private readonly CancellationToken _cancellationToken;

        private readonly RotatingTowerPresenter _towerPresenter;
        public RotateToTargetTask(TowerData towerData, ITowerView view, CancellationToken cancellationToken, RotatingTowerPresenter towerPresenter)
        {
            _towerData = towerData;
            _view = view;
            _cancellationToken = cancellationToken;
            _towerPresenter = towerPresenter;
        }

        async UniTask IPipelineTask.Run(float deltaTime)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => _towerPresenter.Target != null, cancellationToken: _cancellationToken);

                
                var weaponRotation = Rotator.SmoothRotateTowardsTarget(_towerPresenter.Target.Transform.position,
                    _view.WeaponTransform.position, _view.WeaponTransform.rotation, _towerData.RotateSpeed, deltaTime);
                _view.WeaponTransform.rotation = weaponRotation;
                if (ConeDetector.IsTargetInCone(_towerPresenter.Target.Transform.position,
                        _towerPresenter.View.WeaponTransform.position,
                        _towerPresenter.View.WeaponTransform.up, _towerPresenter.TowerData.AttackAngle))
                {
                    break;
                }
                
                await UniTask.NextFrame();
            }
        }
    }

    public sealed class ShootTargetTask : IPipelineTask
    {
        private readonly RotatingTowerPresenter _towerPresenter;

        public ShootTargetTask(RotatingTowerPresenter towerPresenter)
        {
            _towerPresenter = towerPresenter;
        }

        async UniTask IPipelineTask.Run(float deltaTime)
        {
            _towerPresenter.RequestShoot();
            await UniTask.Yield();
        }
    }
}