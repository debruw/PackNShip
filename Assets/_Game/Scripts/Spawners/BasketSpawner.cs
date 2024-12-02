using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BasketSpawner : MonoBehaviour
    {
        public GameObject basketPrefab;
        private GameObject basket;
        
        InventoryController _inventoryController;

        [Inject]
        public void Construct(InventoryController InventoryController)
        {
            Debug.Log("Construct BasketSpawner");
            _inventoryController = InventoryController;
        }

        private void Awake()
        {
            SpawnBox();
        }

        void SpawnBox()
        {
            basket = Instantiate(basketPrefab, transform);
            basket.GetComponent<Basket>().SetInventory(_inventoryController);
            basket.transform.DOLocalMoveY(0, 1f);
        }

        public async UniTask SpawnNewBasket()
        {
            await UniTask.Delay(2000);
            SpawnBox();
        }
    }
}