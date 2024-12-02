using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IStartable
    {
        public ItemGrid itemGrid;

        private InventoryController _inventoryController;

        [Inject]
        public void Construct(InventoryController InventoryController)
        {
            Debug.Log("Constructing Grid Interact");
            _inventoryController = InventoryController;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _inventoryController.SelectedItemGrid = itemGrid;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventoryController.SelectedItemGrid = null;
        }

        public void SetInventory(InventoryController InventoryController)
        {
            _inventoryController = InventoryController;
        }

        public void Start()
        {
            
        }
    }
}