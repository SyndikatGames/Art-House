using UnityEngine;


namespace PrizeClaw
{
    public class ShowEndGameWindow_Event : MonoBehaviour
    {
        [SerializeField] private Claw _claw;
        [SerializeField] private EndGameWindow _endGameWindowPrefab;


        private void OnEnable()
        {
            _claw.onReady += OnClawReady;
        }

        private void OnDisable()
        {
            _claw.onReady -= OnClawReady;
        }


        private void OnClawReady()
        {
            if (GameState.Current.Moves == 0)
                Instantiate(_endGameWindowPrefab, UI.Canvas);
        }


    }
}


