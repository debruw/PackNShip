using System.Collections.Generic;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "ItemsDataList", menuName = "Data/Items Data List", order = 0)]
    public class ItemsDataList : ScriptableObject
    {
        public List<ItemData> itemDatas = new List<ItemData>();
    }
}