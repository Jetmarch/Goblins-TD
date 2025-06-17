using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.GameEngine.Pipeline;
using Game.Gameplay.Storages;
using Game.Gameplay.Towers.Components;
using Modules.Core.GameLoop;

namespace Game.Gameplay.Towers.Presenters
{
    public sealed class RotatingTowerPresenter : ITowerPresenter
    {
        public event Action AttackRequest;
        public ITarget Target => _currentTarget;
        public ITowerView View => _view;
        public TowerData TowerData => _towerData;
        public BulletStorage BulletStorage => _bulletStorage;
        private readonly ITowerView _view;
        private readonly TowerData _towerData;
        
        private ITarget _currentTarget;

        private Pipeline _towerPipeline;
        private PipelineRunner _pipelineRunner;

        private readonly CancellationTokenSource _destroyToken;
        private BulletStorage _bulletStorage;

        public RotatingTowerPresenter(ITowerView view, TowerData towerData, BulletStorage bulletStorage)
        {
            _destroyToken = new CancellationTokenSource();
            _view = view;
            _towerData = towerData;
            _towerPipeline = new Pipeline();
            _pipelineRunner = new PipelineRunner();
            _bulletStorage = bulletStorage;
            
            _towerPipeline.AddTask(new RotateToTargetTask(_towerData, _view, _destroyToken.Token, this));
            _towerPipeline.AddTask(new ShootTargetTask(this, _bulletStorage, _destroyToken.Token, _towerData));
            
            RunPipeline().Forget();
        }

        private async UniTaskVoid RunPipeline()
        {
            while (true)
            {
                await _pipelineRunner.Execute(_towerPipeline);
            }
        }

        public void Destroy()
        {
            _destroyToken.Cancel();
        }
        
        public void SetTarget(ITarget target)
        {
            _currentTarget = target;
        }

        public void LostTarget()
        {
            _currentTarget = null;
        }

        public void RequestShoot()
        {
            AttackRequest?.Invoke();
        }
    }
}