using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSceneDependencies : MonoBehaviour
{
    [field: SerializeField] public RectTransform InventoryRect { get; private set; }
    [field: SerializeField] public RectTransform LevelRect { get; private set; }
    [field: SerializeField] public PressAndFadeMoveCursorTween PressAndFadeMoveCursorPrefab { get; private set; }
    [field: SerializeField] public HoldAndFadeMoveCursorTween HoldAndFadeMoveCursorPrefab { get; private set; }
    [field: SerializeField] public PressMoveCursorTween PressMoveCursorPrefab { get; private set; }
    [field: SerializeField] public TextMeshProUGUI LeftPromptPrefab { get; private set; }
    [field: SerializeField] public TextMeshProUGUI BigCenterPromptPrefab { get; private set; }
    [field: SerializeField] public GameObject CicleScaleCursorPrefab { get; private set; }
    [field: SerializeField] public TextMeshProUGUI RedPromptPrefab { get; private set; }
    [field: SerializeField] public ProgressMarker ProgressMarkerPrefab { get; private set; }
    [field: SerializeField] public GameObject ArrowPromptPrefab { get; private set; }

    [field: SerializeField] public RectTransform ShopRect { get; private set; }
    [field: SerializeField] public TextContainer LeftArrowPromptPrefab { get; private set; }
    [field: SerializeField] public CardInventoryView CardInventory { get; private set; }
    [field: SerializeField] public RectTransform PrizeClawButtonRect { get; private set; }
    [field: SerializeField] public RectTransform ExpandRoomButtonRect { get; private set; }
    [field: SerializeField] public EndTutorialWindowView EndTutorialWindowPrefab { get; private set; }
    [field: SerializeField] public RectTransform StylesButtonRect { get; private set; }
    [field: SerializeField] public List<PlaceGridData> TutorialRoomPlaceGridDataList { get; private set; }
    [field: SerializeField] public Item Room { get; private set; }
    [field: SerializeField] public GameObject RoomPlacePromptPrefab { get; private set; }
    [field: SerializeField] public RectTransform MobileControlRect { get; private set; }
    [field: SerializeField] public Image TakeTaskRewardImage { get; private set; }
    [field: SerializeField] public Image ShopButtonImage { get; private set; }



    public Vector2 InventoryTopCenterPosition => InventoryRect.position + Vector3.up * 140f;



}
