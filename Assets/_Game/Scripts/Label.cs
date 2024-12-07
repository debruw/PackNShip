using System;
using System.Collections.Generic;
using GameTemplate._Game.Scripts.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class Label : MonoBehaviour
    {
        public TextMeshProUGUI _label;
        [HideInInspector]
        public int _orderNumber;

        private Order _order;

        public void SetOrder(Order order)
        {
            _order = order;
            _orderNumber = _order.orderID;
            _label.text = _orderNumber.ToString();
        }

        public bool CheckItems(List<ItemData> itemsInTheBox)
        {
            int counter = 0;
            for (int i = 0; i < _order.orderItems.Count; i++)
            {
                if (itemsInTheBox.Contains(_order.orderItems[i]))
                {
                    counter++;
                }
            }

            if (counter == _order.orderItems.Count)
                return true;

            return false;
        }
    }
}