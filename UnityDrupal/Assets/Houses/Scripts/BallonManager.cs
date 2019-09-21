using UnityEngine;
using Wrld;
using Wrld.Space;

namespace House
{
    public class BallonManager : MonoBehaviour
    {
        //public double latitude = 45.5166999;
        //public double longitude = -73.5204467;
        public GameObject Ballon;

        private GameObject BallonParent;
        private GeographicTransform gt;
        private LatLong targetPosition;

        public void Spawn(double latitude, double longitude)
        {
            targetPosition = new LatLong(latitude, longitude);
            Api.Instance.CameraApi.MoveTo(targetPosition, distanceFromInterest: 1200, headingDegrees: 0, tiltDegrees: 45);

            BallonParent = new GameObject("BallonParent");

            gt = BallonParent.AddComponent(typeof(GeographicTransform)) as GeographicTransform;
            gt.SetPosition(targetPosition);

            Ballon = Instantiate(Ballon, new Vector3(0, 260, 0), Quaternion.identity);
            if (BallonParent !=null)
            {
                Ballon.transform.SetParent(BallonParent.transform);
            }
           

        }


    }
}

