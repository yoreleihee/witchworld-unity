using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LoadManager : MonoBehaviour
{
    [TextArea] public string bundleKey;
    [TextArea] public string objectKey;

    private async void Awake()
    {
        try
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(bundleKey);
            await request.SendWebRequest();

            var bundle = DownloadHandlerAssetBundle.GetContent(request);
            if (bundle == null) throw new NullReferenceException("잘못된 번들입니다.");

            var obj = bundle.LoadAsset<GameObject>(objectKey);
            Instantiate(obj);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
