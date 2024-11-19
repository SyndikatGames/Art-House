using UnityEngine;

namespace VG
{
    public static class TimeHandler
    {
        
        public static void HandlePassedTime(float seconds)
        {

            // ===== Handle time boxes =====
            int offlineSecondsLimit = (int)(TotalRules.OfflineHoursLimit * 3600f);
            seconds = Mathf.Min(seconds, offlineSecondsLimit);
            float boxesPerSecond = TotalRules.BoxesPerHour / 3600f;
            Saves.Float[Key_Save.time_boxes(0)].Value += boxesPerSecond * seconds;

        }



    }
}



