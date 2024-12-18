using System;
using System.Collections.Generic;
using GameTemplate._Game.Scripts.Inventory;
using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    [Serializable]
    public class Order
    {
        public int orderID;
        public List<ItemData> orderItems = new List<ItemData>();
        public GameObject label;
    }
}