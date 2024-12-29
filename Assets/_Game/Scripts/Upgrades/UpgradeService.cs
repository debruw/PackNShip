using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Upgrades
{
    public class UpgradeService : IStartable
    {
        UpgradeGridItem _upgradePrefab;

        UpgradeData _upgradeData;
        PoolingService _poolingService;
        UpgradeCanvasView _upgradeCanvasView;
        
        private readonly IObjectResolver _container;

        public UpgradeService(IObjectResolver container)
        {
            _container = container;
        }

        [Inject]
        public void Construct(UpgradeData upgradeData, PoolingService poolingService,
            UpgradeCanvasView upgradeCanvasView)
        {
            Debug.Log("Construct UpgradeService");

            _upgradeData = upgradeData;
            _poolingService = poolingService;
            _upgradeCanvasView = upgradeCanvasView;
        }

        private void InitializeUpgradeGrid()
        {
            for (int i = 0; i < _upgradeData.upgrades.Count; i++)
            {
                if (!_upgradeData.upgrades[i].isBuyed)
                {
                    CreateAndInitUpgrade(_upgradeData.upgrades[i]);
                }
            }
        }

        private void CreateAndInitUpgrade(UpgradeSO upgradeDataUpgrade)
        {
            _upgradePrefab = _poolingService.GetGameObjectById(PoolID.UpgradeGridItem).GetComponent<UpgradeGridItem>();
            _container.Inject(_upgradePrefab);
            _upgradePrefab.transform.SetParent(_upgradeCanvasView.gridTransform);
            _upgradePrefab.Initialize(upgradeDataUpgrade);
        }

        public void Start()
        {
            Debug.Log("Start UpgradeService");
            InitializeUpgradeGrid();
        }
    }
}