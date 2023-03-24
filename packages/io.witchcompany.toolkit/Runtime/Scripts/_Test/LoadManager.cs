using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public uint crc;
    [TextArea] public string bundleKey;
    [TextArea] public string objectKey;

    private async UniTask LoadBundle(string key)
    {
        try
        {
            // 에셋 번들 다운로드 요청
            using var request = UnityWebRequestAssetBundle.GetAssetBundle(key);
            await request.SendWebRequest();
            
            // 번들 로드
            var bundle = DownloadHandlerAssetBundle.GetContent(request);
            if (bundle == null) throw new NullReferenceException("잘못된 번들입니다.");
        
            // 번들
            var assets = await bundle.LoadAllAssetsAsync();
            Debug.Log(assets.GetType());
        }
        catch (Exception e)
        {
            Debug.LogWarning("에러!:" + e.Message);
            Debug.LogException(e, this);
        }
    }

    private async void Awake()
    {
        try
        {
            // 에셋 번들 다운로드 요청
            using var request = UnityWebRequestAssetBundle.GetAssetBundle(bundleKey);
            await request.SendWebRequest();
            
            // 번들 로드
            var scnBundle = DownloadHandlerAssetBundle.GetContent(request);
            if (scnBundle == null) throw new NullReferenceException("잘못된 번들입니다.");
        
            // 씬 로드
            var scenePaths = scnBundle.GetAllScenePaths();
            if (scenePaths.Length > 0)
            {
                foreach (var scenePath in scenePaths)
                {
                    Debug.Log(System.IO.Path.GetFileNameWithoutExtension(scenePath));
                    if (System.IO.Path.GetFileNameWithoutExtension(scenePath) != objectKey) continue;
                    
                    // 씬 로드 및 활성화
                    await SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
                }
            }
            else
            {
                Debug.LogError("씬이 없습니다");
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("에러!:" + e.Message);
            Debug.LogException(e, this);
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     bundle.Unload(true);
        //     Debug.Log("번들 언로드");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     Destroy(target);
        //     Debug.Log("디스트로이");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     Destroy(instance);
        //     Debug.Log("디스트로이");
        // }
    }
}
