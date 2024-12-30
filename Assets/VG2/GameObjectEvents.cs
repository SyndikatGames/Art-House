using System;
using UnityEngine;

namespace VG2
{
    public class GameObjectEvents : MonoBehaviour
    {
        public event Action onDestroy;


        private void OnDestroy() => onDestroy?.Invoke();



    }
}

