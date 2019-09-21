using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;

namespace House
{
    public class PoinsManager : MonoBehaviour
    {
        //public double latitude = 45.5166999;
        //public double longitude = -73.5204467;
        public GameObject Ballon;
        public GameObject Button;

        private GameObject BallonParent;
        private GeographicTransform gt;
        private LatLong targetPosition;

        public GetPoints getPoints;
        public List<Point> points = new List<Point>();

        bool dataSet = false;

        //public GameObject quad;

        void Start()
        {
            points = getPoints.points;
        }

        void Update()
        {
            if (points.Count == 0 | dataSet)
            {
                return;
            }
            else
            {
                setData();
            }
        }

        void setData()
        {
            var latitude = points[0].Latitude;
            var longitude = points[0].Longitude;
            targetPosition = new LatLong(latitude, longitude);
            Api.Instance.CameraApi.MoveTo(targetPosition, distanceFromInterest: 1200, headingDegrees: 0, tiltDegrees: 45);
 
            for(int i =0; i < points.Count; i++)
            {
                latitude  = points[i].Latitude;
                longitude = points[i].Longitude;
                Spawn(latitude, longitude);
            }

            dataSet = true;
        }

        public void Spawn(double latitude, double longitude)
        {
            BallonParent = new GameObject("BallonParent");
            targetPosition = new LatLong(latitude, longitude);
            gt = BallonParent.AddComponent(typeof(GeographicTransform)) as GeographicTransform;
            gt.SetPosition(targetPosition);
            
            var ballon = Instantiate(Ballon, new Vector3(0, 30, 0), Quaternion.identity);
            var button = Instantiate(Button, new Vector3(0, 80, 0), Quaternion.identity);

            if (BallonParent != null)
            {
                ballon.transform.SetParent(BallonParent.transform);
                button.transform.SetParent(BallonParent.transform);

            }
        }
    }
}



