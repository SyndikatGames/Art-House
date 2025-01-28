using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HoldAndFadeMoveCursorTween : MonoBehaviour
{
    [SerializeField] private Image _cursorImage;
    [SerializeField] private Image _progressImage;

    private RectTransform RectTransform => transform as RectTransform;


    private const float PRESS_DURATION = 0.7f;
    private const float HOLD_DURATION = 0.7f;
    private const float UNPRESSED_SCALE = 1.2f;
    private const float PRESSED_SCALE = 0.8f;
    private const float MOVE_DURATION = 1.5f;


    private Sequence _sequence;

    public void Run(Vector2 from, Vector2 to)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence(transform)
            .AppendCallback(() =>
            {
                RectTransform.localScale = Vector3.one * UNPRESSED_SCALE;
                RectTransform.position = from;
                _progressImage.fillAmount = 0f;
                _cursorImage.color = new Color(_cursorImage.color.r, _cursorImage.color.g, _cursorImage.color.g, 1f);
            })
            .Append(RectTransform.DOScale(PRESSED_SCALE, PRESS_DURATION).SetEase(Ease.OutFlash))
            .Append(_progressImage.DOFillAmount(1f, HOLD_DURATION).SetEase(Ease.Linear))
            .AppendCallback(() => _progressImage.fillAmount = 0f)
            .Append(RectTransform.DOMove(to, MOVE_DURATION).SetEase(Ease.OutFlash))
            .Join(_cursorImage.DOFade(0f, MOVE_DURATION))
            .SetLoops(-1);
    }

    private void OnDestroy() => _sequence?.Kill();





}
