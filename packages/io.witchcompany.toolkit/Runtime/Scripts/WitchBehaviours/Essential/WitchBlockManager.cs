using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchBlockManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 블록 매니저";
        public override string Description => "블록의 기본이 되는 요소입니다.\n" +
                                              "모든 오브젝트의 최상위 부모이며, 기본 좌표에 고정됩니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        [Header("읽기 전용")] 
        [SerializeField, ReadOnly] private List<WitchBehaviour> behaviours;
        [SerializeField, ReadOnly] private WitchSpawnPoint spawnPoint;

        [Header("중력값")]
        [SerializeField, Range(0.1f, 1f)] private float gravity = 1f;
        
        public float GravityRatio => gravity;
        public Transform SpawnPoint => spawnPoint.transform;
        public List<WitchBehaviour> Behaviours => behaviours;

#if UNITY_EDITOR
        /// <summary>위치 요소를 카운팅한 딕셔너리</summary>
        public Dictionary<Type, (WitchBehaviour behaviour, int count)> BehaviourCounter;

        public override ValidationError ValidationCheck()
        {
            if (spawnPoint == null) return NullError(nameof(spawnPoint));
            if (gravity is < 0 or > 1) return Error("gravity는 0과 1 사이어야 합니다.");
            
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
        }
        private void Reset()
        {
            if(!Application.isPlaying) FindWitchBehaviours();
        }
#endif
    }
}