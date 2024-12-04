using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Basket : MonoBehaviour
    {
        ItemGrid itemGrid;
        InventoryController _inventoryController;

        private void Start()
        {
            itemGrid = GetComponentInChildren<ItemGrid>();
            InitObjects();
        }

        void InitObjects()
        {
            for (int i = 0; i < 10; i++)
            {
                _inventoryController.InsertRandomItem(itemGrid);
            }
        }

        public void Empty()
        {
            transform.DOLocalMoveY(500, 1f).SetDelay(1).OnComplete(() => Destroy(gameObject));
            GetComponentInParent<BasketSpawner>().SpawnNewBasket();
        }

        public void InitInventory(InventoryController InventoryController)
        {
            _inventoryController = InventoryController;
            GetComponentInChildren<GridInteract>().SetInventory(InventoryController);
            
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 500);
            transform.DOLocalMoveY(0, 1f).SetDelay(.5f);
        }
    }
}