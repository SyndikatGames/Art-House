using System;
using System.Collections;
using UnityEngine;

namespace VG
{
    public class RealTime : Initializable
    {
        public static float OfflineSeconds { get; private set; }

        public override void Initialize()
        {
            StartCoroutine(Init());
        }

        IEnumerator Init()
        {
            yield return new WaitUntil(() 
                => Saves.Initialized && TotalRules.Initialized);

            OfflineSeconds = HandlePassedTime(useTimeSpan: true);
            Repeater.handlers[Key_Repeat.one_second].onUpdate += OnOneSecondPassed;
            InitCompleted();
        }

        private void OnOneSecondPassed() => HandlePassedTime(useTimeSpan: false);


        private float HandlePassedTime(bool useTimeSpan)
        {
            float passedSeconds = 1f;

            if (useTimeSpan)
            {
                passedSeconds = (float)(DateTime.Now - DateTime.Parse
                    (Saves.String[Key_Save.last_enter_time].Value)).TotalSeconds;
            }

            Saves.String[Key_Save.last_enter_time].Value = DateTime.Now.ToString();
            
            TimeHandler.HandlePassedTime(passedSeconds);
            return passedSeconds;
        }

        
    }
}




