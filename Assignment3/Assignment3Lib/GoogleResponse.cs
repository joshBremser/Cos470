public class GoogleResponse
{
    public location[] locations { get; set; }

}

public class location
{
    public string timestampMs { get; set; }
    public long longitudeE7 { get; set; }
    public long latitudeE7 { get; set; }
    public long accuracy { get; set; }
}
