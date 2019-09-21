using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace House
{

    public class Point
    {
        public double Latitude;
        public double Longitude;

        public string Message { get; }
        public Sprite Image { get; set; }
        public int Count { get; }

        public Point(string message, int count)
        {
            Message = message;
            Count = count;
        }

        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}