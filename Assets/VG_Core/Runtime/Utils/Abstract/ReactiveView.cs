using UnityEngine;


namespace VG
{
    public abstract class ReactiveView : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            Subscribe();
            Display();
        }

        private void OnDisable() => Dispose();

        private void OnDestroy() => Dispose();


        protected abstract void Subscribe();

        protected abstract void Dispose();

        protected abstract void Display();



    }
}


