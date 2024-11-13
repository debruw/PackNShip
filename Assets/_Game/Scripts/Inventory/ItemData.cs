using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public int width = 1;
        public int height = 1;

        public Sprite itemIcon;
    }
}