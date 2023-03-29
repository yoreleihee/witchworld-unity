using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    /// <summary>
    /// Witch Creator Toolkit 에서 사용되는 스크립트의 원본
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class WitchBehaviour : MonoBehaviour
    {
        public abstract string BehaviourName { get; }
        public abstract string Description { get; }
        public abstract string DocumentURL { get; }

       // #if UNITY_EDITOR
       // public abstract bool ValidationCheck();
       // #endif
    }
}
