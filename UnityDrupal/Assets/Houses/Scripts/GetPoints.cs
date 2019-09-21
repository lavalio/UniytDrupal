using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

namespace House
{
    public class GetPoints : MonoBehaviour
    {
        //private string uri = "http://dev-nian.pantheonsite.io/jsonapi/real_estate_property/default";
        private string uri = "http://dev-nian.pantheonsite.io/jsonapi/commerce_store/restaurant";

        double latitude;
        double longitude;

        public List<Point> points = new List<Point>();

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetJSON());
        }

        IEnumerator GetJSON()
        {
            UnityWebRequest www = UnityWebRequest.Get(uri);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                string jsonString = www.downloadHandler.text;
                GetMypoints(jsonString);
            }

            void GetMypoints(string jsonString)
            {
                var jsonObj = JSON.Parse(jsonString);
                var count = jsonObj["meta"]["count"];

                for (int i = 0; i < count; i++)
                {
                    latitude = jsonObj["data"][i]["attributes"]["field_geo_location"]["lat"];
                    longitude = jsonObj["data"][i]["attributes"]["field_geo_location"]["lng"];

                    var point = new Point(latitude, longitude);
                    points.Add(point);
                }
            }
        }
    }
}