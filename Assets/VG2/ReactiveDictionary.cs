using System.Collections;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace VG2
{
    public class ReactiveDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public readonly Subject<Unit> onChanged;
        private Dictionary<TKey, TValue> _dictionary;

        public KeyValuePair<TKey, TValue>[] ToArray() => _dictionary.ToArray();


        public ReactiveDictionary() => _dictionary = new Dictionary<TKey, TValue>();

        public ReactiveDictionary(int capacity) => _dictionary = new Dictionary<TKey, TValue>(capacity);

        public ReactiveDictionary(Dictionary<TKey, TValue> dictionary) => _dictionary = dictionary;



        public int Count => _dictionary.Count;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            onChanged?.OnNext(Unit.Default);
        }

        public void Remove(TKey key)
        {
            if (!_dictionary.ContainsKey(key)) return;

            _dictionary.Remove(key);
            onChanged?.OnNext(Unit.Default);
        }

        public TValue Get(TKey key) => _dictionary[key];

        public void Set(TKey key, TValue value)
        {
            _dictionary[key] = value;
            onChanged?.OnNext(Unit.Default);
        }

        public void Clear()
        {
            _dictionary.Clear();
            onChanged?.OnNext(Unit.Default);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() 
            => ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() 
            => ((IEnumerable)_dictionary).GetEnumerator();

    }
}


