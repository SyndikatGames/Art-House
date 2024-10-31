using UnityEngine;

public class UI : MonoBehaviour
{
    public static Transform Canvas { get; private set; } 


    private void Awake()
    {
        Canvas = transform;
    }

}
