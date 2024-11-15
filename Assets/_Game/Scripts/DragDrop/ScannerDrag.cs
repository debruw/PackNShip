using System;
using GameTemplate._Game.Scripts.Inventory;
using GameTemplate._Game.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class ScannerDrag : DragableUI
    {
        public LabelSpawner LabelSpawner;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out InventoryItem item))
            {
                LabelSpawner.SpawnLabel(item);
            }
        }
    }
}