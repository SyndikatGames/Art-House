using UnityEngine;

namespace VG2
{
    public static class TimeHandler
    {
        
        public static void HandleOfflineTime(float seconds)
        {
            
            /*
            for (int roomIndex = 0; roomIndex < Saves.roomsAmount; roomIndex++)
            {
                
                int offlineSecondsLimit = 
                    (int)(TotalRules.GetOfflineHoursLimit(roomIndex) * 3600f);
                
                
                float newValue = 
                    Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value + seconds;

                Saves.Float[Key_Save.offline_time_seconds(roomIndex)].Value = 
                    Mathf.Min(newValue, offlineSecondsLimit);
                
            }
            */

            HandleTickets(seconds);
            
        }

        public static void HandleOnlineTime(float seconds)
        {
            /*
            for (int roomIndex = 0; roomIndex < Saves.roomsAmount; roomIndex++)
            {
                float boxesPerSecond = TotalRules.GetBoxesPerHour(roomIndex) / 3600f;
                //Saves.Float[Key_Save.random_boxes(0)].Value += boxesPerSecond * seconds;
            }
            */
            HandleTickets(seconds);


        }


        private static void HandleTickets(float seconds)
        {
            float ticketsPerSecond = ConfigHub.PrizeClaw.TicketsPerHour / 3600f;
            float maxTickets = 5f;
            float newValue = GameState.tickets.Value + ticketsPerSecond * seconds;
            GameState.tickets.Value = Mathf.Min(newValue, maxTickets);
        }



    }
}



