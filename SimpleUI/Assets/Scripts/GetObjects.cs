using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

namespace DrupalJSON
{
    public class GetObjects : MonoBehaviour
    {
        private string uri = "http://localhost/web/jsonapi/commerce_product_variation/clothing/";

        public GameObject frame = null;

        MeshRenderer meshRenderer;
 
        void Start()
        {
            StartCoroutine(GetText());
        }

        IEnumerator GetText()
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
                string uri_imge = GetImageAddress(jsonString);
                Debug.Log(uri_imge);
                StartCoroutine(GetTexture(uri_imge));
            }
        }

        IEnumerator GetTexture(string uri)
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
                    ChangeTexture(texture);
                }
            }
        }

        string GetImageAddress(string jsonString)
        {
            var jsonObj = JSON.Parse(jsonString);
            return jsonObj["data"][0]["relationships"]["field_images"]["data"][0]["meta"]["imageDerivatives"]["links"]["product"]["href"];
        }

        void ChangeTexture(Texture albedoTexture)
        {
            meshRenderer = frame.GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_MainTex", albedoTexture);
        }
    }
}
