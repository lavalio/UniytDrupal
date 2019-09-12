using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using SimpleJSON;

namespace FancyScrollView.MonResto
{
    public class GetObj : MonoBehaviour
    {
        private string uri = "http://localhost/web/jsonapi/node/menu";

        //[SerializeField] ScrollView scrollView = default;

        //public List<Texture> images = new List<Texture>();
        public List<Sprite> sprites = new List<Sprite>();
        public List<ItemData> itemDatas = new List<ItemData> ();

        private Texture texture;
        private Sprite mySprite;
        private SpriteRenderer sr;
        private ItemData itemData;

        string jsonString;

     
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
                GetSprites(jsonString);
            }
        }

        void GetSprites(string jsonString)
        {
            var jsonObj = JSON.Parse(jsonString);
            var count = jsonObj["meta"]["count"];
            //Debug.Log("Count:" + count);
            for (int i = 0; i < count; i++)
            {
                string uri_imge = jsonObj["data"][i]["relationships"]["field_menu_images"]["data"][0]["meta"]["imageDerivatives"]["links"]["large"]["href"];
                string description = jsonObj["data"][i]["attributes"]["field_menu_teaser"]["processed"];
                description = description.Remove(0,3);
                var item = new ItemData(description, count);
                StartCoroutine(GetSprite(item,uri_imge));
            }
        }

        IEnumerator GetSprite(ItemData id,string uri)
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
                    mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    sprites.Add(mySprite);

                    id.Image = mySprite;
                    itemDatas.Add(id);
                }
            }
        }
    }
}
