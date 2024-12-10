using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action onHolding;


    private bool _isHolding = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }

    private void Update()
    {
        if (_isHolding) onHolding?.Invoke();
    }

}
