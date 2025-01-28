using R3;

namespace VG2
{
    public class MemoryReactiveProperty<T> : ReactiveProperty<T>
    {
        public MemoryReactiveProperty(T value) : base(value) { }

        public T PreviousValue { get; private set; }


        public override T Value
        {
            get => base.Value;
            set
            {
                PreviousValue = Value;
                base.Value = value;
            }
        }

    }
}



