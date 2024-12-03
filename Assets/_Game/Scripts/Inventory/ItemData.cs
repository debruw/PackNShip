using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public ItemType itemType;
        public int width = 1;
        public int height = 1;

        public Sprite itemIcon;

        public float GetBigSide()
        {
            return width >= height ? width : height;
        }
    }

    public enum ItemType
    {
        None,
        Thumbler,
        Lipstick,
        Parfume,
        Slippers,
        Hoodie,
        Speaker,
        Remote,
        Mug,
        Cutlery,
        Glue
    }
}