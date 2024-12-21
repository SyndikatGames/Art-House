using UnityEngine;
using Zenject;

public class DiContainer : MonoBehaviour
{
    private static DiContainer _instance;

    [Inject] private Zenject.DiContainer _container;

    private void Awake()
    {
        _instance = this;
    }

    public static T Create<T>(T prefab, Transform parent) where T : Object
    {
        var instance = _instance._container.InstantiatePrefab(prefab, parent);
        return instance.GetComponent<T>();
    }


}
