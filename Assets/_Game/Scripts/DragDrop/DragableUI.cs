using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class DragableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Transform targetObject;
        bool _canDrag;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _canDrag = true;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!_canDrag) return;
            
            targetObject.position = eventData.position;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _canDrag = false;
        }
    }
}