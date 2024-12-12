using DG.Tweening;
using UnityEngine;

public class ScaleFlashTween
{
    private const float duration = 0.5f;


    public ScaleFlashTween(GameObject gameObject, float scale)
    {
        var transform = gameObject.transform;
        transform.localScale = Vector3.zero;
        DOTween.Sequence(gameObject.transform)
            .Append(transform.DOScale(scale, duration / 2f).SetEase(Ease.OutFlash))
            .Append(transform.DOScale(0f, duration / 2f).SetEase(Ease.InFlash))
            .AppendCallback(() => Object.Destroy(gameObject));
    }



}
