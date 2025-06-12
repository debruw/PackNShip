using System.Collections.Generic;
using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Utils;
using TMPro;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Basket : MonoBehaviour
    {
        public TextMeshProUGUI barcodeText;
        public Order _order = new Order();

        ItemGrid _itemGrid;
        InventoryController _inventoryController;

        void InitObjects()
        {
            if (UserPrefs.IsFirstPlay())
            {
                List<InventoryItem> items = _inventoryController.InsertItemsForTutorial(_itemGrid);
                for (int i = 0; i < items.Count; i++)
                {
                    _order.orderItems.Add(items[i].itemData);
                }

                return;
            }

            int random = Random.Range(1, 4);
            for (int i = 0; i < random; i++)
            {
                InventoryItem item = _inventoryController.InsertRandomItem(_itemGrid, _order.orderItems);
                if (item != null)
                {
                    _order.orderItems.Add(item.itemData);
                }
            }
        }

        public void Empty()
        {
            transform.DOLocalMoveY(500, 1f).SetDelay(3).OnComplete(
                () =>
                {
                    _inventoryController.GetHighlighterToFillerGrid();
                    Destroy(gameObject);
                });
        }

        public void InitInventory(InventoryController InventoryController, int orderCounter)
        {
            _inventoryController = InventoryController;
            _order.orderID = orderCounter;
            barcodeText.text = "Order " + _order.orderID.ToString("000");
            GetComponentInChildren<GridInteract>().SetInventory(InventoryController);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 500);
            transform.DOLocalMoveY(0, 1f).SetDelay(.5f);

            _itemGrid = GetComponentInChildren<ItemGrid>();
            InitObjects();
        }
    }
}