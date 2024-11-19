using UnityEngine;

public enum GemProductType { RareBox, EpicBox }


public class GemProduct : MonoBehaviour
{
    [field: SerializeField] public GemProductType ProductType { get; private set; }

}
