using System;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private GameObject moveIcon, packButton;
        
        ItemGrid itemGrid;

        private void Awake()
        {
            itemGrid = GetComponentInChildren<ItemGrid>();
        }

        public void PackButtonClick()
        {
            if (itemGrid.IsEmpty())
            {
                return;
            }
            
            packButton.SetActive(false);
            moveIcon.SetActive(true);
        }
    }
}