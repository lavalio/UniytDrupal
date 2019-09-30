using UnityEngine;using SimpleJSON;using System.Collections;using System.Collections.Generic;//using MiniJSON;namespace FancyScrollView.MonResto{    public class GetPoints : MonoBehaviour, IGameManager    {        //[SerializeField] GetSprite Sprite = default;        public ManagerStatus Status { get; private set; }        private NetworkService _network;        private const string jsonApi = "http://dev-nian.pantheonsite.io/jsonapi/real_estate_property/restaurant";        double latitude;        double longitude;        public static List<ItemData> Items = new List<ItemData>();        public void Startup()        {            Debug.Log("Data manager starting...");            _network = new NetworkService();
            GetSprite.myItems = Items;
            StartCoroutine(_network.GetJSONObj(jsonApi, OnJSONDataLoaded));            Status = ManagerStatus.Initializing;        }        // This is the Callback        public void OnJSONDataLoaded(string jsonString)        {            var JsonObj = JSON.Parse(jsonString);            var Count = JsonObj["meta"]["count"];            Debug.Log("Json Object Number:" + Count);            for (int i = 0; i < Count; i++)            {                latitude = JsonObj["data"][i]["attributes"]["field_geo_location"]["lat"];                longitude = JsonObj["data"][i]["attributes"]["field_geo_location"]["lng"];                var geoLocation = new GeoLocation(latitude, longitude);
                var itemData = new ItemData(geoLocation);

                //ItemData itemData = Sprite.itemData = new ItemData(geoLocation);

                //这里真奇怪！var 所定义的是局部变量！！！
                //ItemData itemData = new ItemData(geoLocation);

                string teaser = JsonObj["data"][i]["attributes"]["field_description"]["processed"];                string title = JsonObj["data"][i]["attributes"]["title"];                teaser = teaser.Remove(0, 3);                itemData.Message = teaser;                itemData.Name = title;
                Debug.Log(itemData.Name);
                itemData.Count = Count;

                //Sprite = gameObject.AddComponent<GetSprite>();
                string webImageURL = JsonObj["data"][i]["relationships"]["field_photo"]["data"][0]["meta"]["imageDerivatives"]["links"]["real_estate_property_big"]["href"];
                StartCoroutine(_network.DownloadImage(webImageURL, OnWebImage));
            }            Status = ManagerStatus.Started;        }

        private void OnWebImage(Texture2D texture)
        {
            //GetComponent<Renderer>().material.mainTexture = image;

           Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            //myItems.Add(itemData);
            //Debug.Log("Image manager " + Status);
        }    }}