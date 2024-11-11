using UnityEngine;
using DG.Tweening;


public enum HighlightType { None, Overlap, MergingAvailable, MergingSelect }


public class ItemHighlighter
{
    private Item _item;
    private SpriteRenderer _spriteRenderer;

    private Tween _currentTween;
    private HighlightType _currentHighlightType;

    private const float stopDuration = 0.2f;

    private const float overlapDuration = 0.5f;
    private static readonly Color overlapColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private const int overplapCicleAmount = 2;

    private const float mergingAvailableDuration = 1.5f;
    private static readonly Color mergingAvailableColor = new Color(0.3f, 1f, 0.3f, 1f);

    private static readonly Color mergingSelectColor = new Color(0f, 1f, 0f, 1f);

    public ItemHighlighter(SpriteRenderer spriteRenderer, Item item)
    {
        _spriteRenderer = spriteRenderer;
        _item = item;
    }

    public void Highlight(HighlightType highlightType)
    {
        _currentHighlightType = highlightType;

        switch (highlightType)
        {
            case HighlightType.Overlap:
                HighlightOverlap();
                break;

            case HighlightType.MergingAvailable:
                HighlightMergingAvailable();
                break;

            case HighlightType.MergingSelect:
                HighlightMergingSelect();
                break;
        }
    }


    private void HighlightOverlap()
    {
        _currentTween?.Kill();
        _spriteRenderer.color = Color.white;

        _currentTween = DOTween.Sequence()
            .Append(_spriteRenderer.DOColor(overlapColor, overlapDuration / 2f).SetEase(Ease.Linear))
            .Append(_spriteRenderer.DOColor(Color.white, overlapDuration / 2f).SetEase(Ease.Linear))
            .SetLoops(overplapCicleAmount);

        foreach (var item in _item.ChildItems)
            item.Highlighter.HighlightOverlap();
    }


    private void HighlightMergingAvailable()
    {
        _currentTween?.Kill();
        _spriteRenderer.color = Color.white;

        _currentTween = DOTween.Sequence()
            .Append(_spriteRenderer.DOColor(mergingAvailableColor, mergingAvailableDuration / 2f).SetEase(Ease.Linear))
            .Append(_spriteRenderer.DOColor(Color.white, mergingAvailableDuration / 2f).SetEase(Ease.Linear))
            .SetLoops(-1);
    }

    private void HighlightMergingSelect()
    {
        _currentTween?.Kill();
        _spriteRenderer.color = mergingSelectColor;
    }



    public void Stop(HighlightType highlightType)
    {
        if (_currentHighlightType != highlightType) return;

        _currentTween?.Kill();
        _currentTween = _spriteRenderer.DOColor(Color.white, stopDuration)
            .SetEase(Ease.OutFlash);
    }



}
