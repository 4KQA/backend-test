namespace Nerd.Api.Models
{
    public class GeoLocation
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public GeoLocation(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }


        public string Hemisphere => Latitude switch
        {
            < 0 => "S",
            > 0 => "N",
            _ => ""
        };
    }
}