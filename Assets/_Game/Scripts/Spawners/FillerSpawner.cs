using System;
using System.Collections.Generic;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate.Systems.Pooling;
using UnityEngine;
using VContainer;

namespace GameTemplate._Game.Scripts
{
    public class FillerSpawner : MonoBehaviour
    {
        ItemGrid _itemGrid;
        
        InventoryController _inventoryController;
        PoolingService _poolingService;

        [Inject]
        public void Construct(InventoryController inventoryController, PoolingService poolingService)
        {
            Debug.Log("Construct BasketSpawner");
            _inventoryController = inventoryController;
            _poolingService = poolingService;
            
            GetComponentInChildren<GridInteract>().SetInventory(_inventoryController);
            _itemGrid = GetComponentInChildren<ItemGrid>();
        }

        private void Start()
        {
            SpawnFillingsButtonClick();
        }

        public void SpawnFillingsButtonClick()
        {
            int count = _itemGrid.GetEmptyCount();
            for (int i = 0; i < count; i++)
            {
                _inventoryController.InsertFilling(_itemGrid);
            }
            
        }
    }
}