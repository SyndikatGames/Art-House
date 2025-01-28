using DG.Tweening;
using UnityEngine;

public class RotationTween : MonoBehaviour
{
    [SerializeField] private float _angularSpeed;

    private Tween _currentTween;

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;

        _currentTween = transform.DOLocalRotate(new Vector3(0, 0, 360f), 10f / _angularSpeed, RotateMode.FastBeyond360)
            .SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }



}
