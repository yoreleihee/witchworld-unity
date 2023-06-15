using UnityEngine;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchChasePlayer : WitchBehaviour
    {
        public override string BehaviourName => "효과: 플레이어 따라다니기";
        public override string Description => "자기 자신 오브젝트가 카메라 방향을 바라보게 만듭니다.";
        public override string DocumentURL => "";

        [field: Header("해당 오브젝트가 유저 카메라를 바라보도록, 체크시 바라봄")]
        [field: SerializeField] public bool faceCamera { get; private set; }
        
        [field: Header("텍스트 위치")]
        [field: SerializeField] public Vector3 renderPos { get; private set; }

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            return null;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            
            // 캡슐의 시작과 끝 위치
            Vector3 start = transform.position + transform.up * 1.1f * 0.5f;
            Vector3 end = transform.position - transform.up * 1.1f * 0.5f;

            // 캡슐을 그리기 위한 반지름과 높이 정보
            float scaledRadius = Mathf.Max(transform.lossyScale.x, transform.lossyScale.z) * 0.2f;
            float scaledHeight = Mathf.Max(transform.lossyScale.y, 0f) * 1.1f;

            // DrawGizmo를 사용하여 캡슐 그리기
            Gizmos.DrawWireSphere(start, scaledRadius);
            Gizmos.DrawWireSphere(end, scaledRadius);
            Gizmos.DrawLine(start + transform.right * scaledRadius, end + transform.right * scaledRadius);
            Gizmos.DrawLine(start - transform.right * scaledRadius, end - transform.right * scaledRadius);
            Gizmos.DrawLine(start + transform.forward * scaledRadius, end + transform.forward * scaledRadius);
            Gizmos.DrawLine(start - transform.forward * scaledRadius, end - transform.forward * scaledRadius);
            //Gizmos.DrawWireCube((start + end) * 0.5f, new Vector3(scaledRadius * 2f, scaledHeight, scaledRadius * 2f));
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position + renderPos, 0.1f);
        }
#endif
    }
}
