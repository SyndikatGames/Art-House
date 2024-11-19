using DG.Tweening;
using UnityEngine;

public class ScaleAppearance_Tween : MonoBehaviour
{
    private const float duration = 0.5f;


    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, duration).SetEase(Ease.OutFlash);
    }





}
