using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestImageDownload : MonoBehaviour
{
    public Image image;
    public bool usePolkamonUrl = true;
    public string PolkamonImageUrl = "https://assets.polkamon.com/images/Unimons_T01C05H01B01G00.jpg";
    public string WorkingImageUrl = "https://upload.wikimedia.org/wikipedia/commons/d/db/Patern_test.jpg";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadImage());
    }

    public void OnClickDownloadImage()
    {
        image.sprite = null;
        StartCoroutine(DownloadImage());
    }

    public void OnClickSwitchImage()
    {
        usePolkamonUrl = !usePolkamonUrl;
        OnClickDownloadImage();
    }

    private IEnumerator DownloadImage()
    {
        //Download NFT texture
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(usePolkamonUrl? PolkamonImageUrl : WorkingImageUrl))
        {
            yield return request.SendWebRequest();

            if (request.responseCode >= 400 || request.isHttpError || request.isNetworkError)
            {
                Debug.LogWarning("There was an error downloading your image from " + request.url + " \nError: " + request.error);
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(request);
                image.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                image.SetNativeSize();
            }
            request.Dispose();
        }
    }
}
