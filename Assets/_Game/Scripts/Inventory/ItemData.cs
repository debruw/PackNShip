using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        public Vector2Int size;
        [InfoBox("Structure is starting from top left corner", InfoMessageType.Warning)]
        public Vector2Int[] structure;
        [PreviewField]
        public Sprite itemIcon;
    }
}