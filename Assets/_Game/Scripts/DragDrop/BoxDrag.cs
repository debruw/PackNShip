using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class BoxDrag : DragableUI
    {
        Box _box;
        bool canMove = false;

        private void Start()
        {
            _box = GetComponent /*InParent*/<Box>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            canMove = _box.BeginDrag();
            if (!canMove)
                return;

            base.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (!canMove)
                return;

            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (!canMove)
                return;

            base.OnEndDrag(eventData);
            _box.EndDrag();

            if (RaycastHandler.RaycastTrash(GetComponent<RectTransform>().position))
            {
                if (!_box.IsEmpty)
                {
                    _box.ShowWarning("Not empty!");
                    return;
                }

                /*if (transform.childCount > 0)
                {
                    foreach (Transform child in transform)
                    {
                        child.SetParent(transform.parent);
                    }
                }*/

                _box.DestroyBox();
            }

            if (!_box.isClosed)
            {
                return;
            }

            if (RaycastHandler.RaycastConveyor(GetComponent<RectTransform>().position))
            {
                _box.DeliverBox();
                this.enabled = false;
            }
        }
    }
}