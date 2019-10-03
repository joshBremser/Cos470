using System;

namespace Assignment3Lib
{
    public class Class1
    {
    }

    public class GoogleResponse
    {
        public location[] locations { get; set; }

    }

    public class location
    {
        public int timestampMs { get; set; }
        public int longitudeE7 {get; set;}
        public int latitudeE7 {get; set;}
        public int accuracy {get; set;}

    }
}
