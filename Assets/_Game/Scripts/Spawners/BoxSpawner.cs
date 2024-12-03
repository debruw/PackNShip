using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BoxSpawner : MonoBehaviour
    {
        public GameObject boxPrefab;
        public Transform spawnPoint;
        public TextMeshProUGUI sizeText;

        public Vector2Int boxSize = new Vector2Int(0, 0);
        
        InventoryController _inventoryController;

        [Inject]
        public void Construct(InventoryController InventoryController)
        {
            Debug.Log("Construct BoxSpawner");
            _inventoryController = InventoryController;
        }

        public void SpawnBox()
        {
            GameObject box = Instantiate(boxPrefab, spawnPoint);
            box.GetComponent<Box>().SetSize(boxSize);
            box.GetComponent<Box>()._inventoryController = _inventoryController;
            ItemGrid itemGrid = box.GetComponentInChildren<ItemGrid>();
            itemGrid.GetComponent<GridInteract>().SetInventory(_inventoryController);
            itemGrid.SetSize(boxSize.x, boxSize.y);
        }

        public void NumberButtonClick(int number)
        {
            if (boxSize.x == 0)
            {
                boxSize.x = number;
            }
            else
            {
                boxSize.y = number;
            }

            sizeText.text = "[" + boxSize.x + "," + boxSize.y + "]";
        }

        public void GetBoxButtonClick()
        {
            if (boxSize.x == 0 || boxSize.y == 0) return;
            
            SpawnBox();
            ResetSize();
        }

        public void ResetButtonClick()
        {
            ResetSize();
        }

        void ResetSize()
        {
            boxSize = new Vector2Int(0, 0);
                        sizeText.text = "[" + boxSize.x + "," + boxSize.y + "]";
        }
    }
}