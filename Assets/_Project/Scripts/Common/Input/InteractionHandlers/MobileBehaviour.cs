using UnityEngine;
using UnityEngine.EventSystems;

public partial class InteractionHandler
{
    public class MobileBehaviour : Behaviour
    {
        public static bool Holding { get; private set; }

        private bool _isPointerDown = false;
        private float _pointerDownTime;

        private const float holdingThresholdTime = 0.3f;


        public MobileBehaviour(InteractionHandler handler) : base(handler) { }


        public override void OnPointerDown(PointerEventData eventData)
        {
            Holding = false;
            _pointerDownTime = Time.time;
            _isPointerDown = true;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (Holding)
            {
                beginDragSuccess = true;
                dragging = true;
                handler.onBeginDrag?.Invoke(eventData);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Holding = false;
            base.OnPointerClick(eventData);
        }

        public override void OnUpdate()
        {
            if (_isPointerDown) UpdateHoldingThreshold();
        }

        private void UpdateHoldingThreshold()
        {
            if (Time.time - _pointerDownTime > holdingThresholdTime && !Holding)
            {
                Holding = true;
                handler.onHolding?.Invoke();
                Debug.Log("On holding");
            }
        }



    }


}
