using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

namespace drupalJSON
{
    public class GetObjects_1 : MonoBehaviour
    {
        private string url = "http://localhost/web/jsonapi/commerce_product_variation/clothing";
        //private string href = "";
        //private string href = "http://localhost/web/sites/default/files/styles/product/public/hoodie-blue-1.jpg?itok=7PdRQBxz";
        //private string href = "http://localhost/web/sites/default/files/styles/catalog/public/hoodie-blue-1.jpg";

        void Start()
        {
            StartCoroutine(GetText());
        }

        IEnumerator GetText()
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                string jsonString = www.downloadHandler.text;
                var jsonObj = JSON.Parse(jsonString);
                var type = jsonObj["data"][0]["type"];
                var price = jsonObj["data"][0]["attributes"]["price"]["formatted"];
                string href = jsonObj["data"][0]["relationships"]["field_images"]["data"][0]["meta"]["imageDerivatives"]["links"]["product"]["href"];
                StartCoroutine(GetTexture(href));
            }
        }

        IEnumerator GetTexture(string url)
        {
            Debug.Log("Hi: "+ url);
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Texture myTexture = DownloadHandlerTexture.GetContent(www);
                Debug.Log(myTexture);
            }
        }

    }
}
