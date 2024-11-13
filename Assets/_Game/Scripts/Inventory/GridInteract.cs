using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemGrid itemGrid;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            InventoryController.Instance.SelectedItemGrid = itemGrid;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryController.Instance.SelectedItemGrid = null;
        }
    }
}