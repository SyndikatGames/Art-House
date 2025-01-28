using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PressMoveCursorTween : MonoBehaviour
{

    [SerializeField] private Image _cursorImage;

    private RectTransform RectTransform => transform as RectTransform;


    private const float PRESS_DURATION = 0.6f;
    private const float UNPRESSED_SCALE = 1.2f;
    private const float PRESSED_SCALE = 0.8f;
    private const float MOVE_DURATION = 1f;


    private Sequence _sequence;

    public void Run(Vector2 from, Vector2 to)
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence(transform)
            .AppendCallback(() =>
            {
                RectTransform.localScale = Vector3.one * UNPRESSED_SCALE;
                RectTransform.position = from;
                _cursorImage.color = new Color(_cursorImage.color.r, _cursorImage.color.g, _cursorImage.color.g, 1f);
            })
            .Append(RectTransform.DOScale(PRESSED_SCALE, PRESS_DURATION).SetEase(Ease.OutFlash))
            .Append(RectTransform.DOMove(to, MOVE_DURATION).SetEase(Ease.OutFlash))
            .Append(RectTransform.DOScale(UNPRESSED_SCALE, PRESS_DURATION).SetEase(Ease.OutFlash))
            .Join(_cursorImage.DOFade(0f, PRESS_DURATION).SetEase(Ease.OutFlash))
            .SetLoops(-1);
    }



    private void OnDestroy() => _sequence?.Kill();




}
