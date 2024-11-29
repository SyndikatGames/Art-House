using DG.Tweening;
using UnityEngine;

public class RectCicleMove_Tween : MonoBehaviour
{
    [SerializeField] private Vector2 _rectOffset;

    private const float duration = 1.5f;


    private void OnEnable()
    {
        var rectTransform = GetComponent<RectTransform>();

        DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(rectTransform.anchoredPosition + _rectOffset,
                duration / 2f).SetEase(Ease.InOutFlash))
            .Append(rectTransform.DOAnchorPos(rectTransform.anchoredPosition,
                duration / 2f).SetEase(Ease.InOutFlash))
            .SetLoops(-1);
    }



}
