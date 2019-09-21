using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

namespace House
{
    public class GetHouseInfo : MonoBehaviour
    {
        //private string uri = "http://localhost/web/jsonapi/real_estate_property/default/";
        public string uri;

        double latitude;
        double longitude;

        public SphereManager sm;

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
                string uri_imge = jsonObj["data"][0]["relationships"]["field_360image"]["data"]["meta"]["imageDerivatives"]["links"]["product"]["href"];
                latitude  = jsonObj["data"][0]["attributes"]["field_geo_location"]["lat"];
                longitude = jsonObj["data"][0]["attributes"]["field_geo_location"]["lng"];
                //Debug.Log(uri_imge);
                //Debug.Log(latitude);
                //Debug.Log(longitude);
                StartCoroutine(GetText(uri_imge));
            }
        }

        IEnumerator GetText(string uri)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    // Get downloaded asset bundle
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    //Debug.Log(texture);
                    sm.Spawn(texture, latitude, longitude);

                }
            }
        }

    }
}
