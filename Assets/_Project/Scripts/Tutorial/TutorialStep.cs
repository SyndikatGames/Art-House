using System;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    public event Action onCompleted; public void StepCompleted() => onCompleted?.Invoke();


    public abstract void Run();

    public abstract void RestoreContext();


}
