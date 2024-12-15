using UnityEngine;

namespace GameTemplate._Game.Scripts.Inventory
{
    public class InventoryHighlight : MonoBehaviour
    {
        [SerializeField] private RectTransform highlighter;

        public void SetSize(InventoryItem targetItem)
        {
            Vector2 size = new Vector2();
            size.x = targetItem.Width * GlobalVariables.tileSizeWidth;
            size.y = targetItem.Height * GlobalVariables.tileSizeHeight;
            highlighter.sizeDelta = size;
        }

        public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem)
        {
            Vector2 pos =
                targetGrid.CalculatePositionOnGrid(targetItem, targetItem.onGridPositionX, targetItem.onGridPositionY);

            highlighter.localPosition = pos;
        }

        public void SetPosition(ItemGrid targetGrid, InventoryItem targetItem, int posX, int posY)
        {
            Vector2 pos = targetGrid.CalculatePositionOnGrid(targetItem, posX, posY);

            highlighter.localPosition = pos;
        }

        public void Show(bool show)
        {
            highlighter.gameObject.SetActive(show);
        }

        public void SetParent(ItemGrid targetGrid)
        {
            if (targetGrid == null) return;
            
            highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
        }
    }
}