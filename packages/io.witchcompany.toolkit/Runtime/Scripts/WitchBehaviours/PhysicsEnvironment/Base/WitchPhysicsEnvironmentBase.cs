using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEnvironment
{
    public abstract class WitchPhysicsEnvironmentBase : WitchBehaviourUnique
    {
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            var rb = transform.GetComponentInChildren<Rigidbody>(true);
            if (rb != null)
                return new ValidationError($"{gameObject.name}및 그 자식은 RigidBody를 가질 수 없습니다.", ValidationTag.TagRigidbody, rb);

            var col = transform.GetComponentInChildren<Collider>(true);
            if (col == null)
                return NullError("Collider");
            if (col.gameObject != gameObject)
                return new ValidationError($"{gameObject.name}의 자식 오브젝트는 Collider를 가질 수 없습니다.", ValidationTag.TagScript, col);
            
            if (gameObject.layer != LayerMask.NameToLayer("Default") && gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                return Error($"{gameObject.name}의 Layer는 Default 또는 Ignore Raycast 여야 합니다. 현재({gameObject.layer})");
            
            return null;
        }
#endif
    }
}