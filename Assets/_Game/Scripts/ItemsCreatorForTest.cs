using GameTemplate._Game.Scripts.Inventory;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace GameTemplate._Game.Scripts
{
#if UNITY_EDITOR
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
#endif
}