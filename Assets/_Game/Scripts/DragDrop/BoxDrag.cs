using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTemplate._Game.Scripts
{
    public class BoxDrag : DragableUI
    {
        public static Action OnBoxDelivered;

        Box _box;

        private void Start()
        {
            _box = GetComponent<Box>();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (!_box.isClosed)
            {
                return;
            }

            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (_box.IsEmpty)
            {
                if (RaycastHandler.RaycastTrash())
                {
                    if (transform.childCount > 0)
                    {
                        foreach (Transform child in transform)
                        {
                            child.SetParent(transform.parent);
                        }
                    }

                    Destroy(gameObject);
                    return;
                }
            }

            if (!_box.isLabeled)
            {
                _box.ShowWarning("Not labeled!");
                return;
            }

            if (RaycastHandler.RaycastConveyor())
            {
                //add count
                OnBoxDelivered?.Invoke();

                transform.DOMoveY(transform.position.y - 1080, 3f).OnComplete(() => { Destroy(gameObject); });
                this.enabled = false;
            }
        }
    }
}