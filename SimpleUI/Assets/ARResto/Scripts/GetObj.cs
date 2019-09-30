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
        private string uri = "http://dev-nian.pantheonsite.io/jsonapi/real_estate_property/restaurant";

        //[SerializeField] ScrollView scrollView = default;

        //public List<Texture> images = new List<Texture>();
        public List<Sprite> sprites = new List<Sprite>();
        public List<ItemData> itemDatas = new List<ItemData> ();

        private Texture texture;
        private Sprite mySprite;
        private SpriteRenderer sr;
        private ItemData itemData;

        double latitude;
        double longitude;
        
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
            var JsonObj = JSON.Parse(jsonString);
            var Count = JsonObj["meta"]["count"];
 
            for (int i = 0; i < Count; i++)
            {
                latitude = JsonObj["data"][i]["attributes"]["field_geo_location"]["lat"];
                longitude = JsonObj["data"][i]["attributes"]["field_geo_location"]["lng"];
                var geoLocation = new GeoLocation(latitude, longitude);
                var item = new ItemData(geoLocation);

                string teaser = JsonObj["data"][i]["attributes"]["field_description"]["processed"];
                string title = JsonObj["data"][i]["attributes"]["title"];
                teaser = teaser.Remove(0, 3);

                //var item = new ItemData(teaser,Count);
                item.Message = teaser;
                item.Count = Count;
                item.Name = title;

                string uri_imge = JsonObj["data"][i]["relationships"]["field_photo"]["data"][0]["meta"]["imageDerivatives"]["links"]["real_estate_property_big"]["href"];
    
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

                    id.Sprite = mySprite;
                    itemDatas.Add(id);
                }
            }
        }
    }
}
