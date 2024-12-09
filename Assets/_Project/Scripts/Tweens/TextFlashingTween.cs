using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFlashingTween : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxAlpha;

    private TextMeshProUGUI _text;
    private Tween _currentTween;

    private const float duration = 1.6f;

    private void OnEnable()
    {
        _text ??= GetComponent<TextMeshProUGUI>();

        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _minMaxAlpha.x);

        _currentTween = DOTween.Sequence()
            .Append(_text.DOFade(_minMaxAlpha.y, duration / 2f).SetEase(Ease.Flash))
            .Append(_text.DOFade(_minMaxAlpha.x, duration / 2f).SetEase(Ease.Flash))
            .SetLoops(-1);


    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }

}
