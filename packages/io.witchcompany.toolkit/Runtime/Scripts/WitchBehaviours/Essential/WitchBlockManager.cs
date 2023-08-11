using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchBlockManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 블록 매니저";
        public override string Description => "블록의 기본이 되는 요소입니다.\n" +
                                              "모든 오브젝트의 최상위 부모이며, 기본 좌표에 있어야 합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n" +
                                              "중력값은 기본이 1입니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchBlockManager-27694686287943d28baa4cfbf1803b28?pvs=4";
        public override int MaximumCount => 1;

        [Header("정보")] 
        [SerializeField, ReadOnly] private string unityVersion;
        [SerializeField, ReadOnly] private string toolkitVersion;
        [SerializeField, ReadOnly] private string updatedDateUtc;
        public string UnityVersion => unityVersion;
        public string ToolkitVersion => toolkitVersion;
        public string UpdatedDateUtc => updatedDateUtc;
        
        [Header("연결된 behaviours")]
        [SerializeField, ReadOnly] private List<WitchBehaviour> behaviours;

        [Header("BGM")] 
        [SerializeField] private AudioClip defaultBGM;

        [Header("시점")] 
        [SerializeField] private PointOfView pov = PointOfView.Free;
        public PointOfView POV => pov;
        
        [SerializeField, HideInInspector] private WitchSpawnPoint spawnPoint;
        [Header("캐릭터 리스폰 이벤트"), Tooltip("캐릭터 리스폰되면 이벤트 실행")]
        public UnityEvent respawnEvent;

        public AudioClip DefaultBGM => defaultBGM;
        public Transform SpawnPoint => spawnPoint.transform;
        public List<WitchBehaviour> Behaviours => behaviours;

        public void Respawn() => respawnEvent.Invoke();

#if UNITY_EDITOR
        /// <summary>위치 요소를 카운팅한 딕셔너리</summary>
        public Dictionary<Type, (WitchBehaviour behaviour, int count)> BehaviourCounter { get; private set; }
        
        public override ValidationError ValidationCheck()
        {
            if (spawnPoint == null) return NullError(nameof(spawnPoint));
            if (transform.position != Vector3.zero) return Error("매니저의 좌표는 0이어야 합니다.");
            if (transform.rotation != Quaternion.identity) return Error("매니저의 회전값은 0이어야 합니다.");
            if (transform.localScale != Vector3.one) return Error("매니저의 스케일은 1이어야 합니다.");
            
            return null;
        }

        /// <summary>포함한 위치 요소를 모두 검색하고, 카운팅한다.</summary>
        public void FindWitchBehaviours(string unityVer, string toolkitVer)
        {
            if(Application.isPlaying) return;

            // 날짜 설정
            unityVersion = unityVer;
            toolkitVersion = toolkitVer;
            updatedDateUtc = DateTime.UtcNow.ToString("s");
            
            // 요소 검색
            behaviours = transform.GetComponentsInChildren<WitchBehaviour>(true).ToList();
            behaviours.Remove(this);

            // 스폰포인트 설정
            spawnPoint = behaviours.Find(x => x.GetType() == typeof(WitchSpawnPoint)) as WitchSpawnPoint;
            
            // 카운터 초기화
            BehaviourCounter ??= new Dictionary<Type, (WitchBehaviour obj, int count)>();
            BehaviourCounter.Clear();

            var displayPhotos = new List<WitchDisplayFrame>();
            var displayVideos = new List<WitchDisplayFrame>();
            
            foreach (var behaviour in behaviours)
            {
                // 각 요소별 카운팅
                var type = behaviour.GetType();
                if (!BehaviourCounter.ContainsKey(type))
                    BehaviourCounter.Add(type, (behaviour, 1));
                else
                    BehaviourCounter[type] = (behaviour, BehaviourCounter[type].count + 1);

                // DisplayFrame 카운팅
                if (type == typeof(WitchDisplayFrame))
                {
                    var display = (WitchDisplayFrame) behaviour;
                    if(display.MediaType == MediaType.Picture)
                        displayPhotos.Add(display);
                    else
                        displayVideos.Add(display);
                }
            }
            
            // 액자 인덱스 세팅
            for (var i = 0; i < displayPhotos.Count; i++) 
                displayPhotos[i].Editor_SetIndex(i);
            //비디오 인덱스 세팅
            for (var i = 0; i < displayVideos.Count; i++)
                displayVideos[i].Editor_SetIndex(displayPhotos.Count + i);
        }
#endif
    }
}