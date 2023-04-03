using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        [Header("옵션")] 
        [SerializeField, Range(1, 5)] private float jumpForce = 1;
        [SerializeField, Range(-3,-12)] private float gravity = -12f;

        public float JumpForce => jumpForce;
        public float Gravity => gravity;
        
        public List<WitchBehaviour> Behaviours { get; private set; }

#if UNITY_EDITOR
        /// <summary>위치 요소를 카운팅한 딕셔너리</summary>
        public Dictionary<Type, (WitchBehaviour behaviour, int count)> BehaviourCounter;

        /// <summary>포함한 위치 요소를 모두 검색하고, 카운팅한다.</summary>
        public void FindWitchBehaviours()
        {
            // 위치 초기화
            transform.position = Vector3.zero;
         
            // 요소 검색
            Behaviours = transform.GetComponentsInChildren<WitchBehaviour>(true).ToList();
            Behaviours.Remove(this);
            
            // 요소 카운팅
            BehaviourCounter ??= new Dictionary<Type, (WitchBehaviour obj, int count)>();
            BehaviourCounter.Clear();
            foreach (var behaviour in Behaviours)
            {
                // 각 요소별 카운팅
                var type = behaviour.GetType();
                if (!BehaviourCounter.ContainsKey(type))
                    BehaviourCounter.Add(type, (behaviour, 1));
                else
                    BehaviourCounter[type] = (behaviour, BehaviourCounter[type].count + 1);
            }
        }

        private void OnValidate() => FindWitchBehaviours();
        private void Reset() => FindWitchBehaviours();
#endif
    }
}