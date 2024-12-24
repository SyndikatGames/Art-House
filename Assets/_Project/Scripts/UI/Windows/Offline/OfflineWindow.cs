using TMPro;
using UnityEngine;
using VG2;


public class OfflineWindow : MonoBehaviour
{
    public int BoxesAccumulated { get; private set; }
    public float SoftMoneyAccumulated { get; private set; }




    [SerializeField] private TextMeshProUGUI _offlineTime;

    [SerializeField] private GameObject _boxReward;
    [SerializeField] private TextMeshProUGUI _boxAmountText;

    [SerializeField] private GameObject _softMoneyReward;
    [SerializeField] private TextMeshProUGUI _softMoneyAmountText;



    private void OnEnable()
    {
        UpdateValues();
    }


    private void UpdateValues()
    {
        /*
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;

        float offlineSeconds = Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value;
        _offlineTime.text = $"{Localization.GetString("offline_window_subtitle")} " +
            $"{(int)offlineSeconds} {Localization.GetString("seconds")}";

        float offlineBoxes = TotalRules.GetBoxesPerHour(roomIndex) / 3600f * offlineSeconds;
        float boxesLeftPart = Saves.Float[Key_Save.random_boxes(roomIndex)].Value % 1f;

        BoxesAccumulated = (int)(offlineBoxes + boxesLeftPart);
        _boxReward.SetActive(BoxesAccumulated >= 1f);
        _boxAmountText.text = BoxesAccumulated.ToString();

        SoftMoneyAccumulated = TotalRules.HourGemIncome / 3600f * offlineSeconds ;
        _softMoneyReward.SetActive(SoftMoneyAccumulated >= 1f);
        _softMoneyAmountText.text = ((int)SoftMoneyAccumulated).ToString();

        */
    }

    


}

public static partial class Prefabs
{
    public static OfflineWindow OfflineWindow =>
        Resources.Load<OfflineWindow>("Windows/Offline");
}

