using System.Collections;
using System.Collections.Generic;
using R3;

namespace VG2
{
    public class ReactiveList<T> : IEnumerable<T>
    {
        public readonly Subject<Unit> onChanged = new Subject<Unit>();
        private List<T> _list;


        public ReactiveList() => _list = new List<T>();

        public ReactiveList(int capacity) => _list = new List<T>(capacity);

        public ReactiveList(List<T> list) => _list = list;



        public int Count => _list.Count;

        public void Add(T item)
        {
            _list.Add(item);
            onChanged?.OnNext(Unit.Default);
        }

        public void Remove(T item)
        {
            if (!_list.Contains(item)) return;

            _list.Remove(item);
            onChanged?.OnNext(Unit.Default);
        }

        public T Get(int index) => _list[index];

        public void Set(int index, T value)
        {
            _list[index] = value;
            onChanged?.OnNext(Unit.Default);
        }

        public void Clear()
        {
            _list.Clear();
            onChanged?.OnNext(Unit.Default);
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_list).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();

    }
}



