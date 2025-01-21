using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        [ReadOnly]
        public ItemType itemType;
        [ReadOnly]
        public string itemName;
        [ReadOnly]
        public int width = 1;
        [ReadOnly]
        public int height = 1;
        [ReadOnly]
        public Sprite itemIcon;
    }
}