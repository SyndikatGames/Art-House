using System;
using UnityEngine;

public enum GridType { Floor, ToLeft, ToRight }

public class IsometricGrid : MonoBehaviour
{
    [field: SerializeField] public GridType Type { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; }

    [SerializeField] private Color _gizmosColor;

    private const float edgeSize = 0.0699f;


    private static readonly Vector2 floorAxisX = new Vector2(-0.89415f, -0.44776f) * edgeSize;
    private static readonly Vector2 floorAxisY = new Vector2(0.89415f, -0.44776f) * edgeSize;

    private static readonly Vector2 toLeftAxisX = floorAxisY;
    private static readonly Vector2 toLeftAxisY = Vector2.up * edgeSize;

    private static readonly Vector2 toRightAxisX = floorAxisX;
    private static readonly Vector2 toRightAxisY = Vector2.up * edgeSize;



    public Vector2Int WorldToCell(Vector2 position)
    {
        Vector2 localPosition = position - (Vector2)transform.position;
        // W = position in world, U = basis X, V = basis Y
        // W.U = isometricPointX * (U.U) + isometricPointY * (U.V)
        // W.V = isometricPointX * (U.V) + isometricPointY * (V.V)

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

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;

        Vector2 startPosition = (Vector2)transform.position;
        Vector2 axisX = new Vector2(), axisY = new Vector2();

        switch (Type)
        {
            case GridType.Floor:
                axisX = floorAxisX; axisY = floorAxisY;
                break;

            case GridType.ToLeft:
                axisX = toLeftAxisX; axisY = toLeftAxisY;
                break;

            case GridType.ToRight:
                axisX = toRightAxisX; axisY = toRightAxisY;
                break;
        }

        for (int x = 0; x <= Size.x; x++)
        {
            Vector2 from = startPosition + axisX * x;
            Vector2 to = from + axisY * Size.y;
            Gizmos.DrawLine(from, to);
        }
        for (int y = 0; y <= Size.y; y++)
        {
            Vector2 from = startPosition + axisY * y;
            Vector2 to = from + axisX * Size.x;
            Gizmos.DrawLine(from, to);
        }
    }





}
