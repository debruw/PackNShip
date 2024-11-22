using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Basket : MonoBehaviour
    {
        public InventoryController inventoryController;
        ItemGrid itemGrid;

        private void Start()
        {
            itemGrid = GetComponentInChildren<ItemGrid>();
            InitObjects();
        }

        void InitObjects()
        {
            inventoryController = GetComponentInParent<InventoryController>();
            for (int i = 0; i < 10; i++)
            {
                inventoryController.InsertRandomItem(itemGrid);
            }
        }

        public void Empty()
        {
            transform.DOLocalMoveY(500, 1f).OnComplete(()=>Destroy(gameObject));
            GetComponentInParent<BasketSpawner>().SpawnNewBasket();
        }
    }
}