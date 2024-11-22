using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class TapeDrag : DragableUI
    {
        RectTransform _rect;
        
        private void Start()
        {
            _rect = GetComponent<RectTransform>();
        }

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
                if (box.PutTape(transform))
                {
                    
                    _rect.anchorMin = new Vector2(.5f, .5f);
                    _rect.anchorMax = new Vector2(.5f, .5f);
                    _rect.anchoredPosition = Vector2.zero;
                    GetComponent<Shadow>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}