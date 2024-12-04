using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameTemplate._Game.Scripts
{
    public class DragableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Transform targetObject;
        bool _canDrag;
        private Vector2 _pos;

        RectTransform _rectTransform;
        int _siblingIndex;

        private void Start()
        {
            _rectTransform = targetObject.GetComponent<RectTransform>();
            _siblingIndex = targetObject.GetSiblingIndex();
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _canDrag = true;
            targetObject.SetAsLastSibling();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!_canDrag) return;

            _pos = eventData.position;

            _pos.x = Mathf.Clamp(_pos.x, _rectTransform.rect.width / 2, Screen.width - (_rectTransform.rect.width / 2));
            _pos.y = Mathf.Clamp(_pos.y, _rectTransform.rect.height / 2, Screen.height - (_rectTransform.rect.height / 2));

            targetObject.position = _pos;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _canDrag = false;
            targetObject.SetSiblingIndex(_siblingIndex);
        }
    }
}