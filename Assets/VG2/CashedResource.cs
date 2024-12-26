using UnityEngine;

namespace VG2
{
    public class CashedResource<T> where T : Object
    {
        private string _path;
        private T _cashedValue;

        public CashedResource(string path)
        {
            _path = path;
        }

        public T Value
        {
            get
            {
                if (_cashedValue == null) 
                    _cashedValue = Resources.Load<T>(_path);
                return _cashedValue;

            }
        }

    }
}

