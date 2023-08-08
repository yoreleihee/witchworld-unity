using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.Configs;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Editor.DataStructure.Item;
using WitchCompany.Toolkit.Editor.Tool;

namespace WitchCompany.Toolkit.Editor.GUI
{
    public static class CustomWindowUploadItem
    {
        private static string[] bundleTypes =
        {
            AssetBundleConfig.Webgl,
            AssetBundleConfig.WebglMobile,
            // AssetBundleConfig.Standalone,
            // AssetBundleConfig.Android,
            // AssetBundleConfig.Ios,
            // AssetBundleConfig.Vr
        };
        
        public static void ShowUploadItem()
        {
            DrawUploadItem();
            
            GUILayout.Space(10);
            
            if (GUILayout.Button("Upload"))
            {
                OnClickUpload().Forget();
            }
        }

        private static void DrawUploadItem()
        {
            GUILayout.Label("Upload Item", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            // Bundle Folder
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.TextField("Bundle Folder", ModelConfig.BundleFolderPath);
                if (GUILayout.Button("Select", GUILayout.Width(100)))
                {
                    ModelConfig.BundleFolderPath = EditorUtility.OpenFolderPanel("Witch Creator Toolkit", ModelConfig.BundleFolderPath,"");
                }
            }
            
            // Model File
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.TextField("Gltf File", ModelConfig.GltfPath);
                if (GUILayout.Button("Select", GUILayout.Width(100)))
                {
                    ModelConfig.GltfPath = EditorUtility.OpenFilePanel("Witch Creator Toolkit", "", "gltf");
                }
            } 
            
            // Model Type
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var modelType = (GearType)EditorGUILayout.EnumPopup("Model Type", ModelConfig.ModelType);

                if (check.changed)
                {
                    ModelConfig.ModelType = modelType;
                }
            }
            
            // Disable Body
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var disableBody = (SkinType)EditorGUILayout.EnumFlagsField("Disable BodyType", ModelConfig.DisableBodyType);

                if (check.changed)
                {
                    ModelConfig.DisableBodyType = disableBody;
                }
            }
            EditorGUILayout.EndVertical();
        }

        private static async UniTaskVoid OnClickUpload()
        {
            EditorUtility.DisplayProgressBar("Witch Creator Toolkit", "Uploading to server...", 1.0f);
            
            var result = await UploadItem();
            var msg = result ? AssetBundleConfig.SuccessMsg : AssetBundleConfig.FailedPublishMsg;
            
            EditorUtility.DisplayDialog("Witch Creator Toolkit", msg, "OK");
            EditorUtility.ClearProgressBar();
        }

        private static async UniTask<bool> UploadItem()
        {
            // 비활성화 신체 인덱스 문자열 추출
            var disableBodyToBinary = Convert.ToString(ModelConfig.DisableBodyType.GetHashCode(), 2);
            var disableBodyIndexes = new List<int>();
            var maxIndex = disableBodyToBinary.Length - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                if (disableBodyToBinary[i] == '1')
                    disableBodyIndexes.Add(maxIndex - i);
            }
            
            // 번들 이름
            var bundleName = ModelConfig.BundleFolderPath.Split("/")[^1];
            var typeName = ModelConfig.ModelType.ToString().Replace("Accessory", "Accessory_").ToLower();
            
            // 아이템 정보
            var itemData = new JItemData
            {
                name = bundleName,
                type = typeName,
                bodiesToDisable = JsonConvert.SerializeObject(disableBodyIndexes)
            };

            // 번들 정보
            var bundleInfos = new Dictionary<string, JBundleInfo>();
            foreach (var bundleType in bundleTypes)
            {
                var bundlePath = Path.Combine(ModelConfig.BundleFolderPath, $"{bundleName}_{bundleType}.bundle");
                var bundleInfo = new JBundleInfo();
                var crc = AssetBundleTool.ReadManifest(bundlePath);
                if (crc != null)
                {
                    bundleInfo.unityVersion = ToolkitConfig.UnityVersion;
                    bundleInfo.toolkitVersion = ToolkitConfig.WitchToolkitVersion;
                    bundleInfo.crc = crc;
                }
                bundleInfos[bundleType] = bundleInfo;
            }

            var itemBundleData = new JItemBundleData
            {
                itemData = itemData,
                webgl = bundleInfos[AssetBundleConfig.Webgl],
                webglMobile = bundleInfos[AssetBundleConfig.WebglMobile],
                // standalone = bundleInfos[AssetBundleConfig.Standalone],
                // android = bundleInfos[AssetBundleConfig.Android],
                // ios = bundleInfos[AssetBundleConfig.Ios],
                // vr = bundleInfos[AssetBundleConfig.Vr],
            };
            
            var result = await WitchAPI.UploadItemData(itemBundleData, ModelConfig.BundleFolderPath, ModelConfig.GltfPath);
            
            return result;
        }
    }
}