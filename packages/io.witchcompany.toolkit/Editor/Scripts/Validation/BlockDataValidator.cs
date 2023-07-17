using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class BlockDataValidator
    {
        public static List<JUnityKeyDetail> GetBlockData()
        {
            var art = new JUnityKeyDetail("art");
            var video = new JUnityKeyDetail("video");
            var posting = new JUnityKeyDetail("posting");
            var doodling = new JUnityKeyDetail("doodling");
            var  ranking = new JUnityKeyDetail("ranking");
            
            var unityKeyDetails = new List<JUnityKeyDetail>()
            {
                art,
                video,
                doodling,
                posting,
                ranking
            };
            
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
                // 낙서장
                if (transform.TryGetComponent(out WitchPaintWall paintWall))
                {
                    doodling.count++;
                }
                
                // 포스트잇
                if (transform.TryGetComponent(out WitchPostItWall postItWall))
                {
                    posting.count++;
                }
                // 랭킹보드
                if (transform.TryGetComponent(out WitchLeaderboard leaderboard))
                {
                    ranking.count++;
                }
            }
            
            return unityKeyDetails;
        }
    }
}