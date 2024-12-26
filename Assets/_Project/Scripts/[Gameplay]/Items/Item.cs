using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemPlaceType { Floor, Wall }
public enum Side { Left, Right }


public class Item : MonoBehaviour
{
    [System.Serializable]
    private struct RaritySprite
    {
        public RarityType rarityType;
        public Sprite sprite;
    }

    public event Action onDestroyed;
    public event Action onPlaced;

    [field: SerializeField] public ItemType ItemType { get; private set; }
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Canvas _clickCanvas; 
    public RectTransform CanvasRect => _clickCanvas.GetComponent<RectTransform>();
    

    [field: SerializeField] public ItemPlaceType PlaceType { get; private set; }
    [field: SerializeField] public Vector3Int Size { get; private set; }
    [SerializeField] private List<PlaceGridData> _placeGridDataList = new List<PlaceGridData>();

    [SerializeField] private List<RaritySprite> _sprites; public RarityType OriginRarity => _sprites[0].rarityType;

    public Vector3Int Position { get; private set; }

    public List<PlaceGrid> PlaceGrids { get; private set; }
    public Bounds SpriteBounds => _sprite.bounds;

    public Item ParentItem => ParentGrid == null ? null : ParentGrid.Owner;

    public PlaceGrid ParentGrid { get; private set; }
    public List<Item> ChildItems { get; private set; } = new List<Item>();
    public Side CurrentSide { get; private set; } = Side.Left;
    public RarityType RarityType { get; private set; }

    private ItemEffects _effects;
    public ItemEffects Effects
    {
        get
        {
            _effects ??= new ItemEffects(_sprite, this);
            return _effects;
        }
    }

    public CardModel GetCardModel() => new CardModel
    {
        amount = 1,
        itemType = ItemType,
        rarityType = RarityType,
    };

    public bool BlockRaycast
    {
        get => _clickCanvas.GetComponent<GraphicRaycaster>().enabled;
        set => _clickCanvas.GetComponent<GraphicRaycaster>().enabled = value;
    }

    public bool RarityExists(RarityType rarityType)
    {
        for (int i = 0; i < _sprites.Count; i++)
            if (_sprites[i].rarityType == rarityType)
                return true;

        return false;
    }

    public void SetRarity(RarityType rarityType)
    {
        var raritySprite = _sprites.Find((sprite) => sprite.rarityType == rarityType);

        if (raritySprite.sprite == null)
            raritySprite = _sprites[0];

        _sprite.sprite = raritySprite.sprite;
        RarityType = raritySprite.rarityType;
    }

    public Sprite GetSprite(RarityType rarityType)
    {
        var sprite = _sprites.Find((sprite) => sprite.rarityType == rarityType).sprite;
        if (sprite == null)
            throw new Exception($"[{nameof(Item)}] Wrong sprite rarity type! {ItemType}, {rarityType}");

        return sprite;
    }


    public void SetPlace(Vector3Int position, PlaceGrid placeGrid)
    {
        if (placeGrid != null)
        {
            ParentGrid = placeGrid;
            placeGrid.Owner.ChildItems.Add(this);
        }

        Position = position;
        transform.position = IsometricGrid.GlobalIsometricToCartesian(position);
        UpdatePlaceGrids();

        ItemPlacing.PlacedItems.Add(this);

        onPlaced?.Invoke();
    }

    public void SetPlaceGridData(List<PlaceGridData> dataList)
    {
        _placeGridDataList = dataList;
        UpdatePlaceGrids();
    }

    public PlaceGrid GetPlaceGrid(Vector3Int position, ItemPlaceType placeType, Side side)
    {
        // TODO: It's not universal method!

        foreach (var placeGrid in PlaceGrids)
        {
            if (placeType == ItemPlaceType.Floor && placeGrid.GridType == GridType.Floor)
                return placeGrid;

            if (placeType == ItemPlaceType.Wall)
            {
                if (side == Side.Left && placeGrid.GridType == GridType.ToLeft)
                    return placeGrid;

                if (side == Side.Right && placeGrid.GridType == GridType.ToRight)
                    return placeGrid;
            }
                
        }

        return null;
    }


    public void Placing(Vector3Int position)
    {
        Position = position;
        transform.position = IsometricGrid.GlobalIsometricToCartesian(position);

        bool wasPlaced = ItemPlacing.PlacedItems.Contains(this);

        if (!wasPlaced) ItemPlacing.PlacedItems.Add(this);
        ItemPlacing.ResortOrder();
        if (!wasPlaced) ItemPlacing.PlacedItems.Remove(this);
    }

    public void SetSide(Side side)
    {
        bool sideWasChanged = CurrentSide != side;
        if (sideWasChanged)
        {
            Size = new Vector3Int(Size.y, Size.x, Size.z);
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;

            for (int i = 0; i < _placeGridDataList.Count; i++)
                _placeGridDataList[i] = _placeGridDataList[i].GetOtherSide();

            CurrentSide = side;

            UpdatePlaceGrids();
            ItemPlacing.ResortOrder();
        }
    }

    public bool RotateAvailable
    {
        get
        {
            if (ChildItems.Count != 0) return false;
            if (ItemPlacing.PlacedItems.Contains(this) == false) return true;

            Size = new Vector3Int(Size.y, Size.x, Size.z);
            ItemPlacing.PlacedItems.Remove(this);
            bool available = ParentGrid.TryPlaceItem(this, transform.position, out _);
            Size = new Vector3Int(Size.y, Size.x, Size.z);
            ItemPlacing.PlacedItems.Add(this);
            return available;
        }

    }


    public int SortingOrder
    {
        get
        {
            if (_sprite == null) return 0;
            else return _sprite.sortingOrder;
        }
        set
        {
            if (_sprite == null) return;
            _sprite.sortingOrder = value;
            _clickCanvas.sortingOrder = value;
        }
    }

    private void UpdatePlaceGrids()
    {
        PlaceGrids = new List<PlaceGrid>(_placeGridDataList.Count);
        foreach (var placeGridData in _placeGridDataList)
            PlaceGrids.Add(new PlaceGrid(this, placeGridData, Position));
    }

    public void Destroy()
    {
        ItemPlacing.PlacedItems.Remove(this);
        ItemPlacing.UpdateItems();
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        ParentItem?.ChildItems.Remove(this);
        ItemPlacing.PlacedItems.Remove(this);
        onDestroyed?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.5f;
        Gizmos.color = color;
        
        GridGizmosDrawer.Draw(transform.position, GridType.Floor,
            new Vector2Int(Size.x, Size.y));

        GridGizmosDrawer.Draw(transform.position, GridType.ToLeft,
            new Vector2Int(Size.y, Size.z));

        GridGizmosDrawer.Draw(transform.position, GridType.ToRight,
            new Vector2Int(Size.x, Size.z));

        foreach (var placeGridData in _placeGridDataList)
        {
            Vector2 position = transform.position;
            position += 
                placeGridData.offset.x * IsometricGrid.AxisX +
                placeGridData.offset.y * IsometricGrid.AxisY +
                placeGridData.offset.z * IsometricGrid.AxisZ;

            Gizmos.color = Color.white;
            GridGizmosDrawer.Draw(position, placeGridData.gridType, placeGridData.size);
        }


    }

    private void OnValidate()
    {
        name = ItemType.ToString();
    }

}


public static partial class Prefabs
{
    private static Dictionary<RarityType, List<Item>> _allItems;
    public static Dictionary<RarityType, List<Item>> GetAllRaritySortedItems()
    {
        if (_allItems == null)
        {
            _allItems = new Dictionary<RarityType, List<Item>>();
            foreach (var rarityType in EnumData.GetRarityTypes())
                _allItems.Add(rarityType, new List<Item>());

            var allItems = Resources.LoadAll<Item>("Items");
            foreach (var item in allItems)
                _allItems[item.OriginRarity].Add(item);
        }

        return _allItems;
    }


    public static Item GetItem(ItemType itemType)
        => Resources.Load<Item>($"Items/{itemType}");
}
