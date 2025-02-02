using DG.Tweening;
using UnityEngine;

public class MoveRectTween : MonoBehaviour
{
    [SerializeField] private RectTransform _moveToTransform;

    private RectTransform RectTransform => transform as RectTransform;
    private Vector2? _originPosition = null;
    private bool _opened = false;


    private const float MOVE_DURATION = 0.8f;



    public void Enter()
    {
        if (!_opened)
        {
            if (_originPosition == null)
                _originPosition = transform.position;

            RectTransform.DOMove(_moveToTransform.position, MOVE_DURATION).SetEase(Ease.OutFlash);

            _opened = true;
        }
        
    }

    public void Return()
    {
        if (_opened)
        {
            RectTransform.DOMove((Vector2)_originPosition, MOVE_DURATION).SetEase(Ease.OutFlash);
            _opened = false;
        }
            
    }


}
