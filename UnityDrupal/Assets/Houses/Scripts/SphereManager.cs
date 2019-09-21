using UnityEngine;
using Wrld;
using Wrld.Space;

namespace House
{
    public class SphereManager : MonoBehaviour
    {
        //public double latitude = 45.5166999;
        //public double longitude = -73.5204467;
        public GameObject ReversedSphere;
        //public GetTexture TextureObj;

        private GameObject sphereParent;
        private GeographicTransform gt;
        private LatLong targetPosition;
        private MeshRenderer meshRenderer;

        public void Spawn(Texture texture, double latitude, double longitude)
        {
            targetPosition = new LatLong(latitude, longitude);
            Api.Instance.CameraApi.MoveTo(targetPosition, distanceFromInterest: 1200, headingDegrees: 0, tiltDegrees: 45);

            sphereParent = new GameObject("sphereParent");

            gt = sphereParent.AddComponent(typeof(GeographicTransform)) as GeographicTransform;
            gt.SetPosition(targetPosition);

            ReversedSphere = Instantiate(ReversedSphere, new Vector3(0, 60, 0), Quaternion.identity);
            ReversedSphere.transform.SetParent(sphereParent.transform);
            //var texture = Resources.Load<Texture2D>("Textures/01-7745");
            SetTexture(texture);
        }


        void SetTexture(Texture albedoTexture)
        {
            meshRenderer = ReversedSphere.GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_MainTex", albedoTexture);
        }
    }
}
  
