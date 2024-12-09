using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private Fade_Tween _fadeTween;

    private static Fader _instance;
    private static Action _onFaded;


    private void Awake()
    {
        _instance = this;
    }

    public static void Fade(Action onFaded = null)
    {
        _onFaded = onFaded;
        _instance._fadeTween.onFaded += OnFaded;
        _instance._fadeTween.Fade();
    }

    private static void OnFaded()
    {
        _instance._fadeTween.onFaded -= OnFaded;
        _onFaded?.Invoke();
    }


    public static void Unfade() => _instance._fadeTween.Unfade();



}
