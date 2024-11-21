using System;
using System.Collections;
using UnityEngine;

namespace VG
{
    public class RealTime : Initializable
    {
        public override void Initialize()
        {
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            yield return new WaitUntil(() 
                => Saves.Initialized && TotalRules.Initialized);

            HandleOfflineTime();
            Repeater.handlers[Key_Repeat.one_second].onUpdate += OnOneSecondPassed;
            InitCompleted();
        }

        private void OnOneSecondPassed()
        {
            TimeHandler.HandleOnlineTime(1f);
            Saves.String[Key_Save.last_enter_time].Value = DateTime.Now.ToString();
        }


        private void HandleOfflineTime()
        {
            float passedSeconds = (float)(DateTime.Now - DateTime.Parse
                (Saves.String[Key_Save.last_enter_time].Value)).TotalSeconds;

            Saves.String[Key_Save.last_enter_time].Value = DateTime.Now.ToString();
            
            TimeHandler.HandleOfflineTime(passedSeconds);
        }

        
    }
}




