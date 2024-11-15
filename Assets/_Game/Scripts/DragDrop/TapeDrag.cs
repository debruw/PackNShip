using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class TapeDrag : DragableUI
    {
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            Box box = RaycastHandler.RaycastBox();
            if (box != null)
            {
                if (box.PutTape(transform))
                {
                    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    GetComponent<Shadow>().enabled = false;
                    this.enabled = false;
                }
                else
                {
                    
                }
            }
        }
    }
}