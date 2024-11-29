using UnityEngine;
using VG;

public class ShowBoxOffer_Event : MonoBehaviour
{
    private float _lastShowTime = -showCooldown;

    private const float showCooldown = 60f;





    private void OnEnable()
    {
        Events.onItemPlaced += OnItemPlaced;
    }

    private void OnDisable()
    {
        Events.onItemPlaced -= OnItemPlaced;
    }

    private void OnItemPlaced()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        float randomBoxes = Saves.Float[Key_Save.random_boxes(roomIndex)].Value;
        int boxesAmount = Saves.GetNormalBoxesAmount();

        bool offerAvailable = Time.time - _lastShowTime > showCooldown 
            && Saves.Bool[Key_Save.tutorial_completed].Value;

        if (randomBoxes < 1f && boxesAmount == 0 && offerAvailable)
        {
            _lastShowTime = Time.time;
            Instantiate(Prefabs.BoxOffer, UI.Canvas.transform);
        }


    }




}
