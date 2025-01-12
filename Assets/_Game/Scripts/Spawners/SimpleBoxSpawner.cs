using DG.Tweening;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using TMPro;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class SimpleBoxSpawner : MonoBehaviour
    {
        public Transform spawnPoint;

        public Vector2Int boxSize = new Vector2Int(0, 0);
        private int spawnedBoxCount;

        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct SimpleBoxSpawner");
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
            box.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -600);
            box.SetSize(boxSize);
            box._inventoryController = _inventoryController;

            ItemGrid itemGrid = box.GetComponentInChildren<ItemGrid>();
            itemGrid.GetComponent<GridInteract>().SetInventory(_inventoryController);
            itemGrid.SetSize(boxSize.x, boxSize.y);

            box.GetComponent<RectTransform>().DOAnchorPosY(0, 1);
        }
        
        public void Spawn1X1ButtonClick()
        {
            boxSize.x = 1;
            boxSize.y = 1;
            SpawnBox();
        }
        
        public void Spawn2X2ButtonClick()
        {
            boxSize.x = 2;
            boxSize.y = 2;
            SpawnBox();
        }
        
        public void Spawn3X3ButtonClick()
        {
            boxSize.x = 3;
            boxSize.y = 3;
            SpawnBox();
        }
    }
}