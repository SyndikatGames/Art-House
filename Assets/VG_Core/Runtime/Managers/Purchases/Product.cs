using UnityEngine;

namespace VG2
{
    [System.Serializable]
    public class Product
    {
        [field: SerializeField] public string key { get; private set; }
        [field: SerializeField] public bool consumable { get; private set; }
    }
}

    
