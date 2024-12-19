using System;
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
        private const string Blue = "<color=#2AAAFD>", Orange = "<color=#FF7A19>", White = "<color=white>";

        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct BoxSpawner");
            _inventoryController = inventoryController;
            _poolingService = poolingService;

            Box.OnBoxDestroyed += OnBoxDestroyed;
        }

        private void OnBoxDestroyed()
        {
            spawnedBoxCount--;
        }

        private void OnDestroy()
        {
            Box.OnBoxDestroyed -= OnBoxDestroyed;
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
            
            //ResetSize();
        }

        public void NumberXButtonClick(int number)
        {
            boxSize.x = number;
            SetText();
        }

        public void NumberYButtonClick(int number)
        {
            boxSize.y = number;
            SetText();
        }

        public void GetBoxButtonClick()
        {
            if (boxSize.x == 0 || boxSize.y == 0) return;

            SpawnBox();
            SetText();
        }

        public void ResetButtonClick()
        {
            ResetSize();
        }

        void ResetSize()
        {
            boxSize = new Vector2Int(0, 0);
            SetText();
        }

        void SetText()
        {
            sizeText.text = "[" + Blue + boxSize.x + "," + Orange + boxSize.y + White + "]";
        }
    }
}