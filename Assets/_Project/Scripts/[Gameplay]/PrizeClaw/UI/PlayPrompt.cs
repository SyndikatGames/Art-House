using TMPro;
using UnityEngine;
using VG2;

namespace PrizeClaw
{
    public class PlayPrompt : ReactiveView
    {
        [SerializeField] private TextMeshProUGUI _promptText;


        protected override void Subscribe()
        {
            //Saves.Float[Key_Save.prize_claw_tickets].onChanged += Display;
        }

        protected override void Display()
        {
            /*
            bool gameWasStarted = Saves.String[Key_Save.prize_claw_data].Value != string.Empty;

            if (gameWasStarted)
            {
                _promptText.gameObject.SetActive(true);
                _promptText.text = Localization.GetString("prize_claw_continue");
            }
            else
            {
                bool gameAvailable = Saves.Float[Key_Save.prize_claw_tickets].Value >= 1f;
                _promptText.gameObject.SetActive(gameAvailable);
                _promptText.text = Localization.GetString("prize_claw_available");
            }
            */
            
        }
    }
}

