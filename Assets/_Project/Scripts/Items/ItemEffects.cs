using UnityEngine;
using DG.Tweening;

public class ItemEffects
{
    private Item _item;
    private Material _originMaterial;
    private SpriteRenderer _spriteRenderer;

    private Material _placeOutlineMaterial;
    private Material _highlightMergingMaterial;
    private Material _targetItemMergingMaterial;

    private Tween _currentTween;

    private const float overlapDuration = 0.5f;
    private static readonly Color overlapColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private const int overplapCicleAmount = 2;

    public ItemEffects(SpriteRenderer spriteRenderer, Item item)
    {
        _spriteRenderer = spriteRenderer;
        _item = item;
        _originMaterial = spriteRenderer.material;

        _placeOutlineMaterial = Resources.Load<Material>("Materials/PlaceOutline");
        _highlightMergingMaterial = Resources.Load<Material>("Materials/HighlightMerging");
        _targetItemMergingMaterial = Resources.Load<Material>("Materials/TargetItemMerging");
    }

    public void DragingHiglight()
    {
        DisableCurrentEffect();
        _spriteRenderer.material = _placeOutlineMaterial;
    }


    public void SetTransparent()
    {
        DisableCurrentEffect();

        _spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        _spriteRenderer.sortingLayerName = "Front";
    }

    public void CannotMoveHighlight()
    {
        DisableCurrentEffect();

        _currentTween?.Kill();
        _spriteRenderer.color = Color.white;

        _currentTween = DOTween.Sequence(_item.transform)
            .Append(_spriteRenderer.DOColor(overlapColor, overlapDuration / 2f).SetEase(Ease.Linear))
            .Append(_spriteRenderer.DOColor(Color.white, overlapDuration / 2f).SetEase(Ease.Linear))
            .SetLoops(overplapCicleAmount);

        foreach (var item in _item.ChildItems)
            item.Effects.CannotMoveHighlight();
    }


    public void MergingAvailableHighlight()
    {
        DisableCurrentEffect();
        _spriteRenderer.material = _highlightMergingMaterial;
    }

    public void TargetItemMergingHighlight()
    {
        DisableCurrentEffect();

        _currentTween?.Kill();
        _spriteRenderer.material = _targetItemMergingMaterial;
    }

    public void MergingFlash()
    {
        DisableCurrentEffect();

        var flashPrefab = Resources.Load<GameObject>("Effects/MergeFlash");

        var canvasRect = _item.CanvasRect;
        float flashScale = Mathf.Max(canvasRect.rect.width, canvasRect.rect.height);
        var instObject = Object.Instantiate
            (flashPrefab, canvasRect.transform.position, Quaternion.identity);

        new ScaleFlashTween(instObject, flashScale);
    }



    public void DisableCurrentEffect()
    {
        _currentTween?.Kill();
        _spriteRenderer.material = _originMaterial;
        _spriteRenderer.color = Color.white;
        _spriteRenderer.sortingLayerName = "Item";
    }



}
