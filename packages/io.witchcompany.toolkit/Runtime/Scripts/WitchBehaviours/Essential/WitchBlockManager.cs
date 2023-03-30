using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchBlockManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 블록 매니저";
        public override string Description => "블록의 기본이 되는 요소입니다. 모든 오브젝트의 최상위 부모여야 합니다. 한 씬에 하나만 배치할 수 있습니다.";
        public override string DocumentURL => "";

        [Header("포함된 요소들")]
        [SerializeReference] private List<WitchBehaviour> behaviours;
        
        public List<WitchBehaviour> Behaviours => behaviours;
        
#if UNITY_EDITOR
        private void FindWitchBehaviours()
        {
            transform.position = Vector3.zero;
            behaviours = transform.GetComponentsInChildren<WitchBehaviour>(true).ToList();
            behaviours.Remove(this);
        }
        
        private void OnValidate() => FindWitchBehaviours();
        private void Reset() => FindWitchBehaviours();

        public override bool ValidationCheck()
        {
            FindWitchBehaviours();
            return true;
        }
#endif
    }
}