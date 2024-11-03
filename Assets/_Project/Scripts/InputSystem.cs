using UnityEngine;

public class InputSystem : MonoBehaviour
{

    public delegate void OnMove(Vector2 direction);
    public static event OnMove onMove;

    public delegate void OnScaleChanged(float scaleDelta);
    public static event OnScaleChanged onScaleChanged;

    [SerializeField] private float


    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0f || y != 0f)
            onMove?.Invoke(new Vector2(x, y).normalized);

        if ()


    }





}
