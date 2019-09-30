using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GetTextures: MonoBehaviour
{
    public string href = "";

    void Start()
    {
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(href);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Texture myTexture = DownloadHandlerTexture.GetContent(www);
        }
    }
}