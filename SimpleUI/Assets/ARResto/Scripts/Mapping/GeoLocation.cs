namespace FancyScrollView.MonResto
{
    [System.Serializable]
    public class GeoLocation
    {
        public double Latitude;
        public double Longitude;
        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            //Debug.Log(Latitude);
            //Debug.Log(Longitude);
        }
    }
}
