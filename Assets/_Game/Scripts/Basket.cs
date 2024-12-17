using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Basket : MonoBehaviour
    {
        public TextMeshProUGUI orderText;
        [HideInInspector] public Order _order = new Order();

        ItemGrid _itemGrid;
        InventoryController _inventoryController;

        void InitObjects()
        {
            for (int i = 0; i < 3; i++)
            {
                ItemData itemData = _inventoryController.InsertRandomItem(_itemGrid, _order.orderItems).itemData;
                _order.orderItems.Add(itemData);
            }
        }

        public void Empty()
        {
            transform.DOLocalMoveY(500, 1f).SetDelay(3).OnComplete(
                () =>
                {
                    _inventoryController.GetHighlighterToMainGrid();
                    Destroy(gameObject);
                });
                
        }

        public void InitInventory(InventoryController InventoryController, int orderCounter)
        {
            _inventoryController = InventoryController;
            _order.orderID = orderCounter;
            orderText.text = _order.orderID.ToString();
            GetComponentInChildren<GridInteract>().SetInventory(InventoryController);

            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 500);
            transform.DOLocalMoveY(0, 1f).SetDelay(.5f);
            
            _itemGrid = GetComponentInChildren<ItemGrid>();
            InitObjects();
        }
    }
}