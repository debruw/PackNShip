using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class BoxDrag : DragableUI
    {
        Box _box;

        private void Start()
        {
            _box = GetComponentInParent<Box>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            _box.BeginDrag();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            /*if (!_box.isClosed)
            {
                if (_box.IsEmpty)
                {
                    base.OnDrag(eventData);
                }
                return;
            }*/

            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            _box.EndDrag();
            
            if (RaycastHandler.RaycastTrash())
            {
                if (!_box.IsEmpty)
                {
                    _box.ShowWarning("Not empty!");
                    return;
                }
                
                if (transform.childCount > 0)
                {
                    foreach (Transform child in transform)
                    {
                        child.SetParent(transform.parent);
                    }
                }

                _box.DestroyBox();
            }
            
            if (!_box.isClosed)
            {
                return;
            }

            if (RaycastHandler.RaycastConveyor())
            {
                _box.DeliverBox();
                gameObject.SetActive(false);
            }
        }
    }
}