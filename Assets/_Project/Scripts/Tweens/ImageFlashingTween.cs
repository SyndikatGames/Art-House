using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageFlashingTween : MonoBehaviour
{
    [SerializeField] private Vector2 _minMaxAlpha;
    [SerializeField] private float _cicleDuration;

    private Image _image;
    private Tween _currentTween;

    private void OnEnable()
    {
        _image ??= GetComponent<Image>();

        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _minMaxAlpha.x);

        _currentTween = DOTween.Sequence()
            .Append(_image.DOFade(_minMaxAlpha.y, _cicleDuration / 2f).SetEase(Ease.Flash))
            .Append(_image.DOFade(_minMaxAlpha.x, _cicleDuration / 2f).SetEase(Ease.Flash))
            .SetLoops(-1);


    }

    private void OnDisable()
    {
        _currentTween?.Kill();
    }



}
