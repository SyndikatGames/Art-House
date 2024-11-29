using UnityEngine.EventSystems;

public partial class InteractionHandler
{
    public class DesctopBehaviour : Behaviour
    {
        public DesctopBehaviour(InteractionHandler handler) : base(handler) { }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
            beginDragSuccess = true;
            handler.onBeginDrag?.Invoke(eventData);
        }


    }


}
