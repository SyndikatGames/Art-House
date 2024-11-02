using UnityEngine;

[System.Serializable]
public struct PlaceGridData
{
    public GridType gridType;
    public Vector3Int offset;
    public Vector2Int size;

    public PlaceGridData GetOtherSide()
    {
        PlaceGridData data = this;
        data.offset = new Vector3Int(offset.y, offset.x, offset.z);
        data.size = new Vector2Int(size.y, size.x);
        return data;
    }

}
