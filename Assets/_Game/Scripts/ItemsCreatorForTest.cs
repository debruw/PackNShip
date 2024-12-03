using GameTemplate._Game.Scripts.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class ItemsCreatorForTest : MonoBehaviour
    {
        public ItemGrid mainTableGrid;
        public InventoryController _inventoryController;

        [Button]
        public void CreateObjects()
        {
            _inventoryController.InsertAllItemsEditor(mainTableGrid);
        }
    }
}