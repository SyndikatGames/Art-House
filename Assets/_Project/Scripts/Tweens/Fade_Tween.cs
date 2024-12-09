using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Tween : MonoBehaviour
{
    public event Action onFaded;

    private Image _image;
    private Tween _currentTween;

    private const float fadeDuration = 0.5f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }


    public void Fade()
    {
        gameObject.SetActive(true);

        _image.color = Color.clear;
        _currentTween?.Kill();
        _currentTween = _image.DOFade(1f, fadeDuration).SetEase(Ease.Flash);
        _currentTween.onComplete += () => onFaded?.Invoke();
    }

    public void Unfade()
    {
        _image.color = Color.black;
        _currentTween?.Kill();
        _currentTween = _image.DOFade(0f, fadeDuration).SetEase(Ease.Flash);
        _currentTween.onComplete += () => gameObject.SetActive(false);
    }

}
