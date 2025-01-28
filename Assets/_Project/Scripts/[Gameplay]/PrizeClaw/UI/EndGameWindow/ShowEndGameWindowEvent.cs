using UnityEngine;
using VG2;
using R3;

namespace PrizeClaw
{
    public class ShowEndGameWindowEvent : ReactiveEvent
    {
        [SerializeField] private Claw _claw;
        [SerializeField] private EndGameWindowView _endGameWindowPrefab;

        protected override void Subscribe()
        {
            disposables.Add(_claw.OnReady.Subscribe(_ => OnClawReady()));
        }

        private void OnClawReady()
        {
            if (GameState.prizeClaw.movesLeft.Value == 0)
                SceneContainer.InstantiatePrefabFromComponent(_endGameWindowPrefab, UI.Canvas);
        }

        
    }
}


