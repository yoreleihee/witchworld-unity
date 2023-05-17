using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public abstract class WitchPhysicsEffectBase : WitchBehaviour
    {
        [Header("플레이어와 충돌 여부")] 
        [SerializeField] private bool allowPlayerCollision;

        public bool AllowPlayerCollision => allowPlayerCollision;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            var rb = transform.GetComponentInChildren<Rigidbody>(true);

            if (allowPlayerCollision && GetComponent<Collider>() == null)
                return NullError("Collider");
            
            if (rb != null && rb.gameObject != gameObject)
                return new ValidationError("자식에 RigidBody는 있을 수 없습니다.", ValidationTag.TagRigidbody, this);
            
            if (gameObject.layer != LayerMask.NameToLayer("Default") && gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                return Error($"{gameObject.name}의 Layer는 Default 또는 Ignore Raycast 여야 합니다. 현재({gameObject.layer})");

            return null;
        }
#endif
    }
}