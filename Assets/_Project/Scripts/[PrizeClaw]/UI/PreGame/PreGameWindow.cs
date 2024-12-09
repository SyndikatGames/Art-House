using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;


namespace PrizeClaw
{
    public class PreGameWindow : Info
    {
        [SerializeField] private Color _fadeTicketColor;
        [SerializeField] private List<Image> _ticketImages;

        [SerializeField] private GameObject _playButton;
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private GameObject _bonusButton;
        [SerializeField] private TextMeshProUGUI _nextTicketTimeText;


        private int FullTickets => (int)Saves.Float[Key_Save.prize_claw_tickets].Value;

        private bool GameWasStarted => Saves.String[Key_Save.prize_claw_data].Value != string.Empty;


        protected override void Subscribe()
        {
            Saves.Float[Key_Save.prize_claw_tickets].onChanged += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            Saves.Float[Key_Save.prize_claw_tickets].onChanged -= UpdateValue;
        }

        protected override void UpdateValue()
        {
            UpdateTickets();
            UpdateButtons();
            UpdateTimeText();

        }


        private void UpdateTickets()
        {
            for (int i = 0; i < FullTickets && i < _ticketImages.Count; i++)
                _ticketImages[i].color = Color.white;

            if (FullTickets != _ticketImages.Count)
                _ticketImages[FullTickets].color = _fadeTicketColor;
        }


        private void UpdateButtons()
        {
            if (GameWasStarted)
            {
                _playButton.SetActive(true);
                _playButtonText.text = Localization.GetString("continue");
                _bonusButton.SetActive(false);
            }
            else
            {
                _playButton.SetActive(FullTickets > 0);
                _playButtonText.text = Localization.GetString("play");
                _bonusButton.SetActive(FullTickets < _ticketImages.Count);
            }

        }

        private void UpdateTimeText()
        {
            if (FullTickets == _ticketImages.Count)
                _nextTicketTimeText.gameObject.SetActive(false);

            else
            {
                float notFullTicketPart = Saves.Float[Key_Save.prize_claw_tickets].Value % 1f;
                float ticketsPerSecond = TotalRules.TicketsPerHour / 3600f;
                float oneTicketSecondsRequire = 1 / ticketsPerSecond;

                float secondsLeft = oneTicketSecondsRequire * (1f - notFullTicketPart);

                _nextTicketTimeText.text = Localization.GetString("next_ticket_time_text") +
                    " " + secondsLeft.ToTimeMinutesString();
            }

        }
        
    }

    public static partial class Prefabs
    {
        public static PreGameWindow PreGameWindow =>
            Resources.Load<PreGameWindow>("Windows/PrizeClawPreGame");
    }


}

