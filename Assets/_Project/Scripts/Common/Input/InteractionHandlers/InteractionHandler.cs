using System;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class InteractionHandler : MonoBehaviour, IDragHandler, 
    IBeginDragHandler, IEndDragHandler, IPointerClickHandler, 
    IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerDownHandler
{
    public event Action onHolding;
    public event Action<PointerEventData> onBeginDrag;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onEndDrag;
    public event Action<PointerEventData> onClick;
    public event Action<PointerEventData> onPointerEnter;
    public event Action<PointerEventData> onPointerExit;
    public event Action<PointerEventData> onDrop;

    private Behaviour _behaviour;


    private void Start()
    {
        /*
        if (SystemInfo.deviceType == DeviceType.Desktop)
            _behaviour = new DesctopBehaviour(handler: this);

        else if (SystemInfo.deviceType == DeviceType.Handheld)
            _behaviour = new MobileBehaviour(handler: this);
        */
        _behaviour = new MobileBehaviour(handler: this);
    }

    private void Update() => _behaviour.OnUpdate();


    public bool DisableDragging() => _behaviour.beginDragSuccess = false;


    public void OnBeginDrag(PointerEventData eventData) => _behaviour.OnBeginDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        if (_behaviour.beginDragSuccess)
            _behaviour.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_behaviour.beginDragSuccess)
        {
            _behaviour.dragging = false;
            _behaviour.OnEndDrag(eventData);
        }    
            
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_behaviour.dragging) _behaviour.dragging = false;
        else _behaviour.OnPointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData) 
        => _behaviour.OnPointerEnter(eventData);

    public void OnPointerExit(PointerEventData eventData)
        => _behaviour.OnPointerExit(eventData);

    public void OnDrop(PointerEventData eventData) 
        => _behaviour.OnDrop(eventData);

    public void OnPointerDown(PointerEventData eventData)
       => _behaviour.OnPointerDown(eventData);



}
