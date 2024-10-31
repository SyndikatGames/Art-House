using UnityEngine;

public class TestGrid : MonoBehaviour
{
    [SerializeField] private IsometricGrid grid;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(grid.WorldToCell(worldPosition));
        }

        
    }
}
