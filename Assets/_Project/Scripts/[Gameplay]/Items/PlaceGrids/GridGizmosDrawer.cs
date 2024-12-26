using UnityEngine;

public static class GridGizmosDrawer
{
    
    public static void Draw(Vector2 startPosition, GridType gridType, Vector2Int size)
    {
        Vector2 axisX = new Vector2(), axisY = new Vector2();

        switch (gridType)
        {
            case GridType.Floor:
                axisX = IsometricGrid.AxisX; 
                axisY = IsometricGrid.AxisY;
                break;

            case GridType.ToLeft:
                axisX = IsometricGrid.AxisY; 
                axisY = IsometricGrid.AxisZ;
                break;

            case GridType.ToRight:
                axisX = IsometricGrid.AxisX; 
                axisY = IsometricGrid.AxisZ;
                break;
        }

        for (int x = 0; x <= size.x; x++)
        {
            Vector2 from = startPosition + axisX * x;
            Vector2 to = from + axisY * size.y;
            Gizmos.DrawLine(from, to);
        }
        for (int y = 0; y <= size.y; y++)
        {
            Vector2 from = startPosition + axisY * y;
            Vector2 to = from + axisX * size.x;
            Gizmos.DrawLine(from, to);
        }

    }


}
