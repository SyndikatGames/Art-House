using R3;
using UnityEngine;

namespace VG2
{
    public abstract class ReactiveView : MonoBehaviour
    {
        protected readonly CompositeDisposable disposables = new CompositeDisposable();


        protected virtual void OnEnable()
        {
            Subscribe();
            Display();
        }

        private void OnDisable() => disposables.Dispose();


        protected abstract void Subscribe();

        protected abstract void Display();



    }
}

