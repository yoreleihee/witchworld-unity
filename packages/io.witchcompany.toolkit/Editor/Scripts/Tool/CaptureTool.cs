using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WitchCompany.Toolkit.Editor.Tool
{
    public static class CaptureTool
    {
        public static void CaptureAndSave(Transform cameraParent, string savePath)
        {
            //cameraParent에 카메라 생성
            var cam = new GameObject("Capture Camera").AddComponent<Camera>();
            
            //camera Transform 설정
            cam.transform.SetParent(cameraParent);
            cam.transform.position = cameraParent.position;
            cam.transform.forward = cameraParent.forward;

            //화면 캡쳐
            //카메라 view rt 저장
            var rt = new RenderTexture(Screen.width, Screen.height, 32);
            cam.targetTexture = rt;
            cam.Render();
            
            //Texture2D 생성
            var newTexture = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
            RenderTexture.active = rt;

            //Texture2D로 읽기
            newTexture.ReadPixels(new Rect(0,0,rt.width,rt.height), 0,0);
            newTexture.Apply();

            //rt리셋, 카메라 파괴
            RenderTexture.active = null;
            Object.Destroy(cam);
            
            //캡쳐 파일 저장
            var bytes = newTexture.EncodeToJPG(75);
            var date = DateTime.Now.ToString("yyyy_MM_HH-mm-ss");
            var fileName = "WCapture" + date;
            
            File.WriteAllBytes(savePath, bytes);
        }
    }
}