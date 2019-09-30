using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class NetworkService {

    public IEnumerator GetJSONObj(string url, Action<string> callback) {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        { 
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.LogError("network problem: " + webRequest.error);
            }
            else if (webRequest.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("response error: " + webRequest.responseCode);
            }
            else
            {
                callback(webRequest.downloadHandler.text);
            }
        }
    }
    public IEnumerator DownloadImage(string ImageURL,Action<Texture2D> callback)
    {

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(ImageURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.LogError("network problem: " + webRequest.error);
            }
            else if (webRequest.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("response error: " + webRequest.responseCode);
            }
            else
            {
                callback(DownloadHandlerTexture.GetContent(webRequest));
            }
        }
    }
}
