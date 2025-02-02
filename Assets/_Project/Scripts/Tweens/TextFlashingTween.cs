using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFlashingTween : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxAlpha;
    [SerializeField] private float _cicleDuration;

    private TextMeshProUGUI _text;
    private Tween _currentTween;


    private void OnEnable()
    {
        _text ??= GetComponent<TextMeshProUGUI>();

        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _minMaxAlpha.x);

        _currentTween = DOTween.Sequence()
            .Append(_text.DOFade(_minMaxAlpha.y, _cicleDuration / 2f).SetEase(Ease.Flash))
            .Append(_text.DOFade(_minMaxAlpha.x, _cicleDuration / 2f).SetEase(Ease.Flash))
            .SetLoops(-1);


    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }

}
