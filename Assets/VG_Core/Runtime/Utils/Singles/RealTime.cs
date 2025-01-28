using System;
using System.Collections;
using UnityEngine;

namespace VG2
{
    public class RealTime : Initializable
    {

        public override void Initialize()
        {
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            yield return new WaitUntil(() => Saves.Initialized);

            HandleOfflineTime();
            Repeater.handlers[Key_Repeat.one_second].onUpdate += OnOneSecondPassed;
            InitCompleted();
        }

        private void OnOneSecondPassed()
        {
            TimeHandler.HandleOnlineTime(1f);
            GameState.lastOnlineTime = DateTime.Now;
        }


        private void HandleOfflineTime()
        {
            float passedSeconds = (float)(DateTime.Now - GameState.lastOnlineTime).TotalSeconds;
            GameState.lastOnlineTime = DateTime.Now;
            
            TimeHandler.HandleOfflineTime(passedSeconds);
        }

        
    }
}




