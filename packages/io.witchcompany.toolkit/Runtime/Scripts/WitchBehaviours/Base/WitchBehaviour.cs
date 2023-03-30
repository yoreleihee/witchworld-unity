using System;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    /// <summary>
    /// Witch Creator Toolkit 에서 사용되는 스크립트의 원본
    /// </summary>
    public abstract class WitchBehaviour : MonoBehaviour
    {
        /// <summary>스크립트 이름</summary>
        public abstract string BehaviourName { get; }
        /// <summary>스크립트 설명</summary>
        public abstract string Description { get; }
        /// <summary>스크립트 문서 URL</summary>
        public abstract string DocumentURL { get; }
        /// <summary>스크립트 최대 개수</summary>
        public virtual int MaximumCount => 0;

#if UNITY_EDITOR
        /// <summary>유효성 검사</summary>
        public virtual ValidationError ValidationCheck() => null;

        protected ValidationError ScriptError(string msg) => new(msg, ValidationTag.Script, this);
#endif
    }
}
