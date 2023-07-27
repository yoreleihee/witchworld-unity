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

        [Header("읽기 전용")] 
        [SerializeField, ReadOnly] private List<WitchBehaviour> behaviours;
        [SerializeField, ReadOnly] private WitchSpawnPoint spawnPoint;

        [Header("BGM")] 
        [SerializeField] private AudioClip defaultBGM;
        [Header("중력값")]
        [SerializeField, Range(0.1f, 1f)] private float gravity = 1f;
        
        [HideInInspector] public UnityEvent respawnEvent;

        public AudioClip DefaultBGM => defaultBGM;
        public float GravityRatio => gravity;
        public Transform SpawnPoint => spawnPoint.transform;
        public List<WitchBehaviour> Behaviours => behaviours;

        public void Respawn() => respawnEvent.Invoke();

#if UNITY_EDITOR
        /// <summary>위치 요소를 카운팅한 딕셔너리</summary>
        public Dictionary<Type, (WitchBehaviour behaviour, int count)> BehaviourCounter;

        public override ValidationError ValidationCheck()
        {
            if (spawnPoint == null) return NullError(nameof(spawnPoint));
            if (gravity is < 0 or > 1) return Error("gravity는 0과 1 사이어야 합니다.");
            if (transform.position != Vector3.zero) return Error("매니저의 좌표는 0이어야 합니다.");
            if (transform.rotation != Quaternion.identity) return Error("매니저의 회전값은 0이어야 합니다.");
            if (transform.localScale != Vector3.one) return Error("매니저의 스케일은 1이어야 합니다.");
            
            return null;
        }

        /// <summary>포함한 위치 요소를 모두 검색하고, 카운팅한다.</summary>
        public void FindWitchBehaviours()
        {
            if(Application.isPlaying) return; 
            
            // 요소 검색
            behaviours = transform.GetComponentsInChildren<WitchBehaviour>(true).ToList();
            behaviours.Remove(this);

            spawnPoint = behaviours.Find(x => x.GetType() == typeof(WitchSpawnPoint)) as WitchSpawnPoint;
            
            // 요소 카운팅
            BehaviourCounter ??= new Dictionary<Type, (WitchBehaviour obj, int count)>();
            BehaviourCounter.Clear();
            foreach (var behaviour in behaviours)
            {
                // 각 요소별 카운팅
                var type = behaviour.GetType();
                if (!BehaviourCounter.ContainsKey(type))
                    BehaviourCounter.Add(type, (behaviour, 1));
                else
                    BehaviourCounter[type] = (behaviour, BehaviourCounter[type].count + 1);
            }
        }
        private void OnValidate()
        {
            if(!Application.isPlaying) FindWitchBehaviours();
            
            SetDisplayIndex();
            
        }
        private void Reset()
        {
            if(!Application.isPlaying) FindWitchBehaviours();
            
            SetDisplayIndex();
        }

        private void SetDisplayIndex()
        {
            //Debug.Log("setIndex?");
            //var displayFrames = FindObjectsOfType<WitchDisplayFrame>();
            var displayFrames = GetComponentsInChildren<WitchDisplayFrame>();
            //Debug.Log(displayFrames.Length);
            if(displayFrames.Length == 0) return;
            
            var pictures = new List<WitchDisplayFrame>();
            var videos = new List<WitchDisplayFrame>();

            foreach (var display in displayFrames)
            {
                 switch (display.MediaType)
                 {
                     case MediaType.Picture:
                     {
                         //Debug.Log("pic?");
                         pictures.Add(display);
                         break;
                     }
                     case MediaType.Video:
                     {
                         videos.Add(display);
                         break;
                     }
                 }
            }

            if(pictures.Count > 0)
                for (int i = 0; i < pictures.Count; ++i)
                {
                    //Debug.Log(i);
                    pictures[i].SetIndex(i);
                }

            if(videos.Count > 0)
                for (int i = 0; i < videos.Count; ++i)
                {
                    videos[i].SetIndex(i);
                }
        }
#endif
    }
}