using System;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class ScannerDrag : DragableUI
    {
        public LabelSpawner LabelSpawner;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Barcode barcode))
            {
                Order _order = barcode.GetComponentInParent<Basket>()._order;
                if (_order.label == null)
                {
                    _order.label = LabelSpawner.SpawnLabel(_order);
                    barcode.FlashRed();
                }
            }
        }
    }
}