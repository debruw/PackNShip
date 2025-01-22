using Cysharp.Threading.Tasks;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using GameTemplate.Utils;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BasketSpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;

        private Basket basket;

        int orderCounter = 0;
        bool canSpawn = true;

        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct BasketSpawner");
            _inventoryController = inventoryController;
            _poolingService = poolingService;

            Timer.OnTimesUp += OnTimesUp;
        }

        private void OnTimesUp()
        {
            canSpawn = false;
            Timer.OnTimesUp -= OnTimesUp;
        }

        private void Awake()
        {
            SpawnBasket();
        }

        void SpawnBasket()
        {
            if (!canSpawn)
                return;

            Transform parent = GetParent();

            if (parent != null)
            {
                orderCounter++;
                basket = _poolingService.GetGameObjectById(PoolID.BasketPrefab).GetComponent<Basket>();
                basket.transform.SetParent(parent);
                basket.GetComponent<Basket>().InitInventory(_inventoryController, orderCounter);
            }

            if (!UserPrefs.IsFirstPlay())
            {
                SpawnNewBasket(5000);
            }
        }

        public Transform GetParent()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].childCount == 0)
                {
                    return spawnPoints[i];
                }
            }

            return null;
        }

        public async UniTask SpawnNewBasket(int waitTime = 2000)
        {
            await UniTask.Delay(waitTime);
            SpawnBasket();
        }
    }
}