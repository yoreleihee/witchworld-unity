using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class AssetDataValidator
    {
        private static JUnityKeyDetail art = new("art");
        private static JUnityKeyDetail video = new("video");
        private static JUnityKeyDetail posting = new("posting");
        private static JUnityKeyDetail doodling = new("doodling");
        private static JUnityKeyDetail ranking = new("ranking");
        private static JUnityKeyDetail freeArt = new("freeArt");
        private static JUnityKeyDetail stall = new("stall");
        
        private static Dictionary<string, JUnityKeyDetail> assetData = new ()
        {
            {"art", art},
            {"video", video},
            {"posting", posting},
            {"doodling", doodling},
            {"ranking", ranking},
            {"freeArt", freeArt},
            {"stall", stall}
        };
        
        public static Dictionary<string, JUnityKeyDetail> GetAssetData()
        {
            Initialize();
            
            var transforms = GameObject.FindObjectsOfType<Transform>(true);

            foreach (var transform in transforms)
            {
                // 전시
                if (transform.TryGetComponent(out WitchDisplayFrame displayFrame))
                {
                    // 사진
                    if (displayFrame.MediaType == MediaType.Picture)
                        art.count++;
                    // 비디오
                    else if (displayFrame.MediaType == MediaType.Video)
                        video.count++;
                }
                //자유배치 에셋
                if (transform.TryGetComponent(out WitchFreeDisplay freeDisplay))
                {
                    //art.count++;
                    freeArt.count++;
                }

                //가판대 에셋
                if (transform.TryGetComponent(out WitchStallDisplay stallDisplay))
                {
                    stall.count++;
                }
                
                // 낙서장
                if (transform.TryGetComponent(out WitchPaintWall paintWall))
                    doodling.count++;
                
                // 포스트잇
                if (transform.TryGetComponent(out WitchPostItWall postItWall))
                    posting.count++;
                
                // 랭킹보드
                if (transform.TryGetComponent(out WitchLeaderboard leaderboard))
                    ranking.count++;
            }
            
            return assetData;
        }

        private static void Initialize()
        {
            foreach (var key in assetData.Keys.ToList())
                assetData[key].count = 0;
        }
    }
}