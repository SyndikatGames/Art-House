using DG.Tweening;
using UnityEngine;

public class Flash_Tween : MonoBehaviour
{
    private const float duration = 0.5f;


    public void Run(float scale)
    {
        transform.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(transform.DOScale(scale, duration / 2f).SetEase(Ease.OutFlash))
            .Append(transform.DOScale(0f, duration/ 2f).SetEase(Ease.InFlash))
            .AppendCallback(() => Destroy(gameObject));
    }



}
