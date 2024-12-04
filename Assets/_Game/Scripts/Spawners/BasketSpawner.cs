using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BasketSpawner : MonoBehaviour
    {
        private Basket basket;

        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct BasketSpawner");
            _inventoryController = inventoryController;
            _poolingService = poolingService;
        }

        private void Awake()
        {
            SpawnBox();
        }

        void SpawnBox()
        {
            basket = _poolingService.GetGameObjectById(PoolID.BasketPrefab).GetComponent<Basket>();
            basket.transform.SetParent(transform);
            basket.GetComponent<Basket>().InitInventory(_inventoryController);
        }

        public async UniTask SpawnNewBasket()
        {
            await UniTask.Delay(2000);
            SpawnBox();
        }
    }
}