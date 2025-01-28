using DG.Tweening;
using UnityEngine;

public class MoveRectTween : MonoBehaviour
{
    [SerializeField] private Vector2 _moveOffset;

    private RectTransform RectTransform => transform as RectTransform;
    private Vector2 _originPosition;
    private Vector2? _targetPosition;
    private bool _opened = false;


    private const float MOVE_DURATION = 0.8f;



    private void Start()
    {
        _originPosition = RectTransform.position;
    }


    public void Enter()
    {
        if (!_opened)
        {
            print(RectTransform.position);

            if (_targetPosition == null)
                _targetPosition = (Vector2)RectTransform.position + _moveOffset;

            RectTransform.DOMove((Vector2)_targetPosition, MOVE_DURATION).SetEase(Ease.OutFlash);

            _opened = true;
        }
        
    }

    public void Return()
    {
        if (_opened)
        {
            RectTransform.DOMove(_originPosition, MOVE_DURATION).SetEase(Ease.OutFlash);
            _opened = false;
        }
            
    }


}
