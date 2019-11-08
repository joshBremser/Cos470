using System;
using static GoogleResponse;
namespace Assignment3Lib
{
    public class GoogleMethods
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        //Check Alibi A way to check where you were on a particular day
        public static location checkAlibi(GoogleResponse response, DateTime time)
        {
            foreach (location l in response.locations)
            {
                DateTime t = UnixTimeStampToDateTime(double.Parse(l.timestampMs));
                if (Math.Abs((t - time).TotalDays) < 2) 
                    return l;
            }
            return null;
        }
        //Have we met? A way to find collisions between location histories
        public static bool haveWeMet(GoogleResponse response, GoogleResponse otherResponse)
        {
            foreach (location l in otherResponse.locations)
            {
                foreach (location p in response.locations)
                {
                    if (l.latitudeE7 == p.latitudeE7 &&
                        l.longitudeE7 == p.longitudeE7 &&
                        l.timestampMs == p.timestampMs)
                        return true;
                }
            }
            return false;
        }

    }

}
