using DG.Tweening;
using UnityEngine;

public class CursorMove_Tween : MonoBehaviour
{
    private const float duration = 1.5f;


    public void Run(Vector2 fromPosition, Vector2 toPosition)
    {
        transform.position = fromPosition;
        transform.DOMove(toPosition, duration).SetEase(Ease.InOutFlash)
            .SetLoops(-1);
    }


}
