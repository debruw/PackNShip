using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class DragableUI : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        public Transform targetObject;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            targetObject.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}