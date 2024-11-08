using System;
using UnityEngine;

namespace VG
{
    public abstract class TimeHandler : MonoBehaviour
    {
        protected float OfflineSeconds { get; private set; }
        protected abstract string TimeSaveKey { get; }


        private void OnEnable()
        {
            OfflineSeconds = HandlePassedTime(useTimeSpan: true);
            Repeater.handlers[Key_Repeat.one_second].onUpdate += OnOneSecondPassed;
        }

        private void OnDisable()
        {
            Repeater.handlers[Key_Repeat.one_second].onUpdate -= OnOneSecondPassed;
        }

        private void OnOneSecondPassed() => HandlePassedTime(useTimeSpan: false);


        private float HandlePassedTime(bool useTimeSpan)
        {
            float passedSeconds = 1f;

            if (useTimeSpan)
            {
                passedSeconds = (float)(DateTime.Now - DateTime.Parse
                    (Saves.String[TimeSaveKey].Value)).TotalSeconds;
            }

            Saves.String[TimeSaveKey].Value = DateTime.Now.ToString();
            OnTimePassed(passedSeconds);
            return passedSeconds;
        }

        protected abstract void OnTimePassed(float seconds);



    }
}



