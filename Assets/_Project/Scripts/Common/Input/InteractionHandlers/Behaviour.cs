using System;
using UnityEngine.EventSystems;

public partial class InteractionHandler
{
    public abstract class Behaviour
    {
        protected InteractionHandler handler;
        public bool dragging;
        public bool beginDragSuccess;

        public Behaviour(InteractionHandler handler)
        {
            this.handler = handler;
            dragging = false;
            beginDragSuccess = false;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
            => handler.onBeginDrag?.Invoke(eventData);

        public virtual void OnDrag(PointerEventData eventData)
            => handler.onDrag?.Invoke(eventData);

        public virtual void OnEndDrag(PointerEventData eventData)
            => handler.onEndDrag?.Invoke(eventData);

        public virtual void OnPointerClick(PointerEventData eventData)
            => handler.onClick?.Invoke(eventData);

        public virtual void OnPointerEnter(PointerEventData eventData)
            => handler.onPointerEnter?.Invoke(eventData);

        public virtual void OnPointerExit(PointerEventData eventData)
            => handler.onPointerExit?.Invoke(eventData);

        public virtual void OnDrop(PointerEventData eventData)
            => handler.onDrop?.Invoke(eventData);

        public virtual void OnPointerDown(PointerEventData eventData) { }

        public virtual void OnUpdate() { }


    }
}


