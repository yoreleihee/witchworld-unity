using System;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [RequireComponent(typeof(SphereCollider))]
    public class WitchPortal : WitchBehaviourUnique
    {
        [Obsolete("더이상 사용하지 않습니다.")]
        public string TargetUrl => "";
        
        public override string BehaviourName => "통로: 포탈";
        public override string Description => "다른 실내/실외 공간으로 연결되는 포탈입니다.\n" +
                                              "입구로 사용될 SphereCollider가 필요합니다.(IsTrigger 필요).\n" +
                                              "입구와 출구는 겹치면 안됩니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchPortal-18bc652f7fb9436a8db9a624925c2689?pvs=4";
        public override int MaximumCount => 2;

        [Header("출구")]
        [SerializeField] private Transform exit;
        public Transform Exit => exit;


        public static WitchPortal InstantiateDefaultPortal(Transform parent, Vector3 pos, Quaternion rot, string portalName)
        {
            var portalObj = new GameObject(portalName).transform;
            var exitObj = new GameObject("Exit").transform;
            
            portalObj.transform.SetParent(parent);
            portalObj.transform.SetPositionAndRotation(pos, rot);
            
            exitObj.SetParent(portalObj);
            exitObj.transform.localPosition = Vector3.forward * 1f;
            
            var portal = portalObj.gameObject.AddComponent<WitchPortal>();
            portal.GetComponent<SphereCollider>().isTrigger = true;
            portal.exit = exitObj;

            return portal;
        }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (exit == null)
                return NullError(nameof(exit));
            if (!TryGetComponent(out SphereCollider col))
                return NullError("SphereCollider");

            if (col.radius <= 0)
                return Error("포탈 입구의 크기는 0보다 커야 합니다.");
            if (!col.isTrigger)
                return TriggerError(col);

            var distance = Vector3.Distance(exit.position, transform.position);
            if (distance < col.radius + 0.5f)
                return Error("포탈 입구와 출구가 겹칩니다. 입구에서 최소 0.5m 이상 떨어지게 해 주세요.");

            return base.ValidationCheck();
        }
        
        [UnityEditor.MenuItem("GameObject/WitchToolkit/Portal", false, 0)]
        public static void CreateDefaultObject(UnityEditor.MenuCommand menuCommand)
        {
            // 1. Custom GameObject 이름으로 새 Object를 만든다.
            var portal = new GameObject("Witch Portal").AddComponent<WitchPortal>();
            var exit = new GameObject("Exit");

            // 2. Hierachy 윈도우에서 어떤 오브젝트를 선택하여 생성시에는 그 오브젝트의 하위 계층으로 생성된다.
            // 그밖의 경우에는 아무일도 일어나지 않는다.
            UnityEditor.GameObjectUtility.SetParentAndAlign(portal.gameObject, menuCommand.context as GameObject);
            UnityEditor.GameObjectUtility.SetParentAndAlign(exit, portal.gameObject);
            
            // 설정
            exit.transform.localPosition = Vector3.forward * 1f;
            portal.exit = exit.transform;
            portal.GetComponent<SphereCollider>().isTrigger = true;
  
            // // 3. 생성된 오브젝트를 Undo 시스템에 등록한다.
            // UnityEditor.Undo.RegisterCreatedObjectUndo(portal, "Create " + portal.name);
  
            // 4. 생성한 오브젝트를 선택한다.
            UnityEditor.Selection.activeObject = portal;
        }
#endif
    }
}