using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class BoxSpawner : MonoBehaviour
    {
        public Transform spawnPoint;
        public TextMeshProUGUI sizeText;

        public Vector2Int boxSize = new Vector2Int(0, 0);
        private int spawnedBoxCount;

        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct BoxSpawner");
            _inventoryController = inventoryController;
            _poolingService = poolingService;

            Box.OnBoxDestroyed += () => spawnedBoxCount--;
        }

        public void SpawnBox()
        {
            if (spawnedBoxCount > 10)
                return;

            spawnedBoxCount++;

            Box box = _poolingService.GetGameObjectById(PoolID.BoxPrefab).GetComponent<Box>();
            box.transform.SetParent(spawnPoint);
            box.transform.localPosition = Vector3.zero;
            box.SetSize(boxSize);
            box._inventoryController = _inventoryController;

            ItemGrid itemGrid = box.GetComponentInChildren<ItemGrid>();
            itemGrid.GetComponent<GridInteract>().SetInventory(_inventoryController);
            itemGrid.SetSize(boxSize.x, boxSize.y);
        }

        public void NumberXButtonClick(int number)
        {
            boxSize.x = number;

            sizeText.text = "[<color=\"red\">" + boxSize.x + "<color=\"blue\">," + boxSize.y + "<color=\"white\">]";
        }

        public void NumberYButtonClick(int number)
        {
            boxSize.y = number;

            sizeText.text = "[<color=\"red\">" + boxSize.x + "<color=\"blue\">," + boxSize.y + "<color=\"white\">]";
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
            sizeText.text = "[<color=\"red\">" + boxSize.x + "<color=\"blue\">," + boxSize.y + "<color=\"white\">]";
        }
    }
}