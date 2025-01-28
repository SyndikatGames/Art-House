using System;
using R3;

public abstract class TutorialStep
{
    public event Action onCompleted; public void StepCompleted() => onCompleted?.Invoke();

    protected TutorialSceneDependencies Dependencies { get; private set; }
    protected TaskController TaskController { get; private set; }

    protected readonly CompositeDisposable Disposables = new CompositeDisposable();
    

    public void SetDependecies(TutorialSceneDependencies dependencies, TaskController taskController)
    {
        Dependencies = dependencies;
        TaskController = taskController;
    }



    public abstract void Run();

    public virtual void RestoreContext()
    {
        Disposables.Dispose();
    }




}
