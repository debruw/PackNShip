using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class DragableUI : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        public Transform targetObject;
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            targetObject.position = eventData.position;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}