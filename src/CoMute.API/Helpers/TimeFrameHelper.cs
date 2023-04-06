using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Helpers
{
    public class TimeFrameHelper
    {
        public static bool CheckTimeFrames(DateTime departureTime1,DateTime arrivalTime1,DateTime departureTime2,DateTime arrivalTime2)
        {
            // checks if the opportunity is on the same date for departure and arrival
            if (departureTime1.Date == departureTime2.Date && arrivalTime1.Date == arrivalTime2.Date)
            {
                //if the opportunity is on the same date then check if the time frames  on that date overlaps
                if ((departureTime1.TimeOfDay <= arrivalTime2.TimeOfDay) && (arrivalTime1.TimeOfDay >= departureTime2.TimeOfDay))
                    return false;
                else
                    return true;
            }

            if ((departureTime1 <= arrivalTime2) && (arrivalTime1 >= departureTime2))
                return false;
            else
                return true;

        }
    }
}
