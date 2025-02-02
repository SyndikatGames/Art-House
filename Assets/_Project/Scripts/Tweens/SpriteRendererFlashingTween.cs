using DG.Tweening;
using UnityEngine;

public class SpriteRendererFlashingTween : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxAlpha;
    [SerializeField] private float _cicleDuration;

    private SpriteRenderer _sprite;
    private Tween _currentTween;

    private void OnEnable()
    {
        _sprite ??= GetComponent<SpriteRenderer>();

        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _minMaxAlpha.x);

        _currentTween = DOTween.Sequence()
            .Append(_sprite.DOFade(_minMaxAlpha.y, _cicleDuration / 2f).SetEase(Ease.Flash))
            .Append(_sprite.DOFade(_minMaxAlpha.x, _cicleDuration / 2f).SetEase(Ease.Flash))
            .SetLoops(-1);


    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }



}
