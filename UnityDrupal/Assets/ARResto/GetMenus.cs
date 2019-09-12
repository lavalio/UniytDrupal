using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;

namespace FancyScrollView.MonResto
{
    public class GetMenus : MonoBehaviour
    {
        //private string uri = "http://localhost/web/jsonapi/commerce_product_variation/clothing/";
        private string uri = "http://localhost/web/jsonapi/node/menu";
        public List<Texture> images = new List<Texture>();

        public GameObject frame = null;
        string jsonString;
        int index = 0;
        int count;

        MeshRenderer meshRenderer;
 
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
                GetImages(jsonString);
            }
        }

        void GetImages(string jsonString)
        {
            var jsonObj = JSON.Parse(jsonString);
            count = jsonObj["meta"]["count"];
            Debug.Log("Conut:"+count);
            for (int i =0; i<count; i++)
            {
                string uri_imge = jsonObj["data"][i]["relationships"]["field_menu_images"]["data"][0]["meta"]["imageDerivatives"]["links"]["large"]["href"];
                StartCoroutine(GetImage(uri_imge));
            }
        }


        IEnumerator GetImage(string uri)
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
                    //ChangeTexture(texture);
                    images.Add(texture);
                }
            }
        }

        void Update()
        {
            if (count == 0) return;

            if (index < count)
            {
                index++;
            }else
            {
                index = 0;
            }

            //Debug.Log(count);
            Debug.Log(index);
            StartCoroutine(Wait());

            //var texture = images[2];
            //ChangeTexture(texture);
        }
      

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(30);
    }

    void ChangeTexture(Texture albedoTexture)
        {
            meshRenderer = frame.GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_MainTex", albedoTexture);
        }
    }
}
