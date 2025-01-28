using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMarker : MonoBehaviour
{
    [SerializeField] private Image _progressFill;
    [SerializeField] private Image _doneMarker;

    private Sequence _sequence;

    private const float DONE_SCALE_FROM = 0f;
    private const float DONE_SCALE_TO = 1f;
    private const float DONE_ANIM_DURATION = 0.5f;


    public void SetProgress(float progress)
    {
        _progressFill.fillAmount = progress;
        _doneMarker.gameObject.SetActive(false);
    }

    public void Done()
    {
        _progressFill.gameObject.SetActive(false);
        _doneMarker.gameObject.SetActive(true);

        _doneMarker.transform.localScale = Vector3.one * DONE_SCALE_FROM;

        _sequence = DOTween.Sequence()
            .Append(_doneMarker.transform.DOScale(DONE_SCALE_TO, DONE_ANIM_DURATION).SetEase(Ease.OutFlash))
            .Append(_doneMarker.DOFade(0f, DONE_ANIM_DURATION).SetEase(Ease.OutFlash))
            .AppendCallback(() => Destroy(gameObject));
    }

    private void OnDisable()
    {
        _sequence?.Kill();
    }


}
