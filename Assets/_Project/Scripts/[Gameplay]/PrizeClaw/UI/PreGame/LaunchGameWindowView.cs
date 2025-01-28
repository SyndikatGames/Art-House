using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;
using R3;


namespace PrizeClaw
{
    public class LaunchGameWindowView : ReactiveView
    {
        [SerializeField] private Color _fadeTicketColor;
        [SerializeField] private List<Image> _ticketImages;

        [SerializeField] private GameObject _playButton;
        [SerializeField] private TextMeshProUGUI _playButtonText;
        [SerializeField] private GameObject _bonusButton;
        [SerializeField] private TextMeshProUGUI _nextTicketTimeText;

        public RectTransform PlayButtonRect => _playButton.transform as RectTransform;

        private int FullTickets => (int)GameState.tickets.Value;


        protected override void Subscribe()
        {
            disposables.Add(GameState.tickets.Subscribe(_ => Display()));
        }


        protected override void Display()
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
            if (GameState.prizeClaw.gameStarted)
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
                float notFullTicketPart = GameState.tickets.Value % 1f;
                float ticketsPerSecond = ConfigHub.PrizeClaw.TicketsPerHour / 3600f;
                float oneTicketSecondsRequire = 1 / ticketsPerSecond;

                float secondsLeft = oneTicketSecondsRequire * (1f - notFullTicketPart);

                _nextTicketTimeText.text = Localization.GetString("next_ticket_time_text") +
                    " " + secondsLeft.ToTimeMinutesString();
            }

        }
        
    }

}

