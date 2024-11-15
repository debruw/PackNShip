using System;
using System.Collections.Generic;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Basket : MonoBehaviour
    {
        public InventoryController inventoryController;
        ItemGrid itemGrid;

        private void Start()
        {
            itemGrid = GetComponentInChildren<ItemGrid>();
            InitObjects();
        }

        void InitObjects()
        {
            for (int i = 0; i < 10; i++)
            {
                inventoryController.InsertRandomItem(itemGrid);
            }
        }
    }
}