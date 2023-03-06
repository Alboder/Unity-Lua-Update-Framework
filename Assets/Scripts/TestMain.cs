using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class TestMain : MonoBehaviour
{
    private const string AssetURL = "http://192.168.1.21/aaa/MainText.txt";
    private void Start()
    {
        //Download
#if UNITY_STANDALONE_WIN
        Debug.Log("WINDOW");
#elif UNITY_ANDROID
        Debug.Log("ANDROID");
#elif UNITY_IOS
        Debug.Log("IOS");
#endif
    }

    IEnumerator DownloadAssets()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AssetURL))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                
            }

        }
    }
}
