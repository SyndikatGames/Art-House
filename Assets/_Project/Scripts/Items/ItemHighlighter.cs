using UnityEngine;
using DG.Tweening;

public class ItemHighlighter
{
    private SpriteRenderer _spriteRenderer;

    private Tween _currentTween;

    private const float duration = 0.5f;
    private const int cicleAmount = 2;

    private static readonly Color fadeColor = new Color(0.5f, 0.5f, 0.5f, 1f);


    public ItemHighlighter(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }



    public void Highlight()
    {
        _currentTween?.Kill();

        _currentTween = DOTween.Sequence()
            .Append(_spriteRenderer.DOColor(fadeColor, duration / 2f).SetEase(Ease.OutFlash))
            .Append(_spriteRenderer.DOColor(Color.white, duration / 2f).SetEase(Ease.OutFlash))
            .SetLoops(cicleAmount);
    }



}
