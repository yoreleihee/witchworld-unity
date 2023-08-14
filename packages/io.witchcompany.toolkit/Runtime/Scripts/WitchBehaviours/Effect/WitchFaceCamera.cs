using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.PhysicsEffect
{
    public class WitchFaceCamera : WitchBehaviour
    {
        public override string BehaviourName => "효과: 카메라 방향 보기";
        public override string Description => "자기 자신 오브젝트가 카메라 방향을 바라보게 만듭니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchFaceCamera-197feb9114a446e3bbbd194e67c9d8ce?pvs=4";

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            return null;
        }
#endif
    }
}