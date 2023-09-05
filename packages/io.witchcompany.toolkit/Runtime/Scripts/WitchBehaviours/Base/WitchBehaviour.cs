using UnityEngine;
using UnityEngine.Events;
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
        public virtual ValidationReport ValidationCheckReport() => null;

        protected ValidationError Error(string msg) => new(msg, ValidationTag.TagScript, this);
        protected ValidationError NullError(string scriptName) => Error($"{name}의 {scriptName}를 설정해주세요.");
        protected ValidationError ChildError(string scriptName) => Error($"{name}의 {scriptName}는 자식 오브젝트여야 합니다.");
        protected ValidationError TriggerError(Collider col) => Error($"{name}의 콜라이더({col.name})는 IsTrigger 체크되어야 합니다.");
        protected ValidationError NotTriggerError(Collider col) => Error($"{name}의 {col.name}은 IsTrigger 체크될 수 없습니다.");
        protected ValidationError RayLayerError(GameObject target) =>
            Error($"{name}의 {target.name}의 레이어는 Ignore Raycast일 수 없습니다.");
        protected ValidationError StaticError(GameObject target) => Error($"{name}의 static을 해제해야 합니다.");
        protected ValidationError EventHandlerCheck(UnityEvent unityEvent)
        {
            for (var i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                var target = unityEvent.GetPersistentTarget(i);

                if (target == null)
                    return NullError("Event");
                if (target.GetType() != typeof(GameObject)
                    && target is not MonoBehaviour
                    && target is not Animator)
                    return Error($"{name}에 {target}은 연결시킬 수 없습니다.");
            }

            return null;
        }
#endif
    }
}
