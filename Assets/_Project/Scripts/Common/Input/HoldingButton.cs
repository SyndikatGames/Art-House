using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action onBeginHolding;
    public event Action onHolding;
    public event Action onEndHolding;

    private bool _isHolding = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
        onBeginHolding?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
        onEndHolding?.Invoke();
    }

    private void Update()
    {
        if (_isHolding) onHolding?.Invoke();
    }

}
