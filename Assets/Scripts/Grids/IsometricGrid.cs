using UnityEngine;

public enum GridType { Floor, ToLeft, ToRight }


public abstract class IsometricGrid
{
    protected GridType gridType;
    protected Vector2Int size;
    protected Vector3Int position;

    private const float edgeSize = 0.0699f;


    public static readonly Vector2 floorAxisX = new Vector2(-0.89415f, -0.44776f) * edgeSize;
    public static readonly Vector2 floorAxisY = new Vector2(0.89415f, -0.44776f) * edgeSize;

    public static readonly Vector2 toLeftAxisX = floorAxisY;
    public static readonly Vector2 toLeftAxisY = Vector2.up * edgeSize;

    public static readonly Vector2 toRightAxisX = floorAxisX;
    public static readonly Vector2 toRightAxisY = Vector2.up * edgeSize;


    private Vector2 GetWorldPosition()
    {
        Vector2 result = Vector2.zero;

        result += floorAxisX * position.x;
        result += floorAxisY * position.y;
        result += toRightAxisY * position.z;

        return result;
    }


    protected void CartesianToIsometric(Vector2 position, 
        out Vector2Int local, out Vector3Int world)
    {
        Vector2 localPosition = position - GetWorldPosition();
        Vector2 W = localPosition, U = Vector2.zero, V = Vector2.zero;

        switch (gridType)
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

        int X = 0, Y = 0, Z = 0;

        switch (gridType)
        {
            case GridType.Floor:
                X = (int)x; Y = (int)y;
                break;

            case GridType.ToLeft:
                Y = (int)x; Z = (int)y;
                break;

            case GridType.ToRight:
                X = (int)x; Z = (int)y;
                break;
        }

        local = new Vector2Int((int)x, (int)y);
        world = new Vector3Int(X, Y, Z);
    }

    protected Vector2 CellToWorld(Vector2Int gridPosition)
    {
        Vector2 result = Vector2.zero;

        switch (gridType)
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

        return result + GetWorldPosition();
    }



}
