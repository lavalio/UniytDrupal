using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

namespace House
{
    public class GetLocation : MonoBehaviour
    {
        //private string uri = "http://localhost/web/jsonapi/real_estate_property/default/";
        private string uri = "http://dev-nian.pantheonsite.io/jsonapi/real_estate_property/default";
        //public string uri;

        double latitude;
        double longitude;

        public BallonManager bm;

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
                //Debug.Log(jsonString);
                var jsonObj = JSON.Parse(jsonString);
                //Debug.Log(jsonObj["data"][0]["relationships"]["field_360image"]["data"]["meta"]);

                latitude  = jsonObj["data"][0]["attributes"]["field_geo_location"]["lat"];
                longitude = jsonObj["data"][0]["attributes"]["field_geo_location"]["lng"];
                Debug.Log(latitude);
                Debug.Log(longitude);
                bm.Spawn(latitude, longitude);
            }
        }
    }
}
