using UnityEngine;
using System.Collections.Generic;

namespace FancyScrollView.MonResto
{
    public class GetSprite : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }
        private NetworkService _network;
        public string webImageURL { get; set; }
        public static Sprite Sprite { get; set; }
        public ItemData itemData;
        public static List<ItemData> myItems;

        public GetSprite(ItemData id) {
            itemData = id;
        }

        public void Startup()
        {
            Debug.Log("Image manager starting...");
            _network = new NetworkService();
            StartCoroutine(_network.DownloadImage(webImageURL, OnWebImage));
            Status = ManagerStatus.Started;
        }

        private void OnWebImage(Texture2D texture)
        {
            //GetComponent<Renderer>().material.mainTexture = image;
           
            Sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f); 
            //myItems.Add(itemData);
            //Debug.Log("Image manager " + Status);
        }
    }
}

