using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public class WitchBlockManager : WitchBehaviour
    {
        public override string BehaviourName => "블록 매니저";
        public override string Description => "블록을 관리해주는 컴포넌트. 모든 오브젝트의 최상위 오브젝트여야 합니다.";
        public override string DocumentURL => "";

        [SerializeReference] private List<WitchBehaviour> behaviours;
        
#if UNITY_EDITOR
        private void FindWitchBehaviours()
        {
            behaviours = transform.GetComponentsInChildren<WitchBehaviour>(true).ToList();
            behaviours.Remove(this);
        }
        
        private void OnValidate()
        {
            transform.position = Vector3.zero;

            FindWitchBehaviours();
        }

        private void Reset()
        {
            FindWitchBehaviours();
        }
#endif
    }
}