using UnityEngine;

namespace FancyScrollView.MonResto
{
    [System.Serializable]
    public class ItemData
    {
        public string Message { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public int Count { get; set; }
        public GeoLocation geoLocation;

        public ItemData()
        {

        }


        public ItemData(GeoLocation geoLoc)
        {
            geoLocation = geoLoc;
        }

        public ItemData(string message, int count)
        {
            Message = message;
            Count = count;
        }
    }
}
