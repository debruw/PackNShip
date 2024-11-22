using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class LabelDrag : DragableUI
    {
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            if (RaycastHandler.RaycastTrash())
            {
                Destroy(gameObject);
                return;
            }
            
            Box box = RaycastHandler.RaycastBox();
            if (box != null)
            {
                if (box.PutLabel(transform))
                {
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    GetComponent<Shadow>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}