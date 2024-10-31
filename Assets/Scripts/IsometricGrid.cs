using UnityEngine;

public enum GridType { Floor, ToLeft, ToRight }

public class IsometricGrid : MonoBehaviour
{
    [field: SerializeField] public GridType Type { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; }

    private const float edgeSize = 0.0699f;


    public static readonly Vector2 floorAxisX = new Vector2(-0.89415f, -0.44776f) * edgeSize;
    public static readonly Vector2 floorAxisY = new Vector2(0.89415f, -0.44776f) * edgeSize;

    public static readonly Vector2 toLeftAxisX = floorAxisY;
    public static readonly Vector2 toLeftAxisY = Vector2.up * edgeSize;

    public static readonly Vector2 toRightAxisX = floorAxisX;
    public static readonly Vector2 toRightAxisY = Vector2.up * edgeSize;



    public Vector2Int WorldToCell(Vector2 position)
    {
        Vector2 localPosition = position - (Vector2)transform.position;
        Vector2 W = localPosition, U = Vector2.zero, V = Vector2.zero;

        switch (Type)
        {
            case GridType.Floor:
                U = floorAxisX; V = floorAxisY;
                break;

            case GridType.ToLeft:
                U = toLeftAxisX; V = toLeftAxisY;
                break;

            case GridType.ToRight:
                U = toRightAxisX; V = toRightAxisY;
                break;
        }


        float WU = Vector2.Dot(W, U), WV = Vector2.Dot(W, V),
            UU = Vector2.Dot(U, U), UV = Vector2.Dot(U,V), VV = Vector2.Dot(V, V);

        float a = (UU * WV - WU * UV)/(UU * VV);
        float b = 1 - (UV * UV / (UU * VV));

        float y = a / b;
        float x = (WU - y * UV) / UU;

        if (y < 0f) y -= 1f;
        if (x < 0f) x -= 1f;
        
        return new Vector2Int((int)x, (int)y);
    }

    public Vector2 CellToWorld(Vector2Int gridPosition)
    {
        Vector2 result = Vector2.zero;

        switch (Type)
        {
            case GridType.Floor:
                result = floorAxisX * gridPosition.x + floorAxisY * gridPosition.y;
                break;

            case GridType.ToLeft:
                result = toLeftAxisX * gridPosition.x + toLeftAxisY * gridPosition.y;
                break;

            case GridType.ToRight:
                result = toRightAxisX * gridPosition.x + toRightAxisY * gridPosition.y;
                break;
        }

        return result + (Vector2)transform.position;
    }



}
