using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public abstract class WitchPhysicsEffectBase : WitchBehaviour
    {
        [Header("플레이어와 충돌 여부")] 
        [SerializeField] private bool allowPlayerCollision;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            var rb = transform.GetComponentInChildren<Rigidbody>(true);
            if (rb != null && rb.gameObject != gameObject)
                return new ValidationError("자식에 RigidBody는 있을 수 없습니다.", ValidationTag.TagRigidbody, this);
            
            if (!gameObject.CompareTag("Untagged"))
                return Error($"{gameObject.name}의 태그는 Untagged 여야 합니다. 현재({gameObject.tag})");
            if (gameObject.layer != LayerMask.NameToLayer("Default") && gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                return Error($"{gameObject.name}의 Layer는 Default 또는 Ignore Raycast 여야 합니다. 현재({gameObject.layer})");

            return null;
        }
#endif
    }
}