using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchSpawnPoint : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 스폰 포인트";
        public override string Description => "플레이어의 시작점이 되는 지점입니다. 한 씬에 하나만 배치할 수 있습니다.";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchSpawnPoint-97dd84399a924c6bb0fae62890db063a?pvs=4";
        public override int MaximumCount => 1;
                
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (transform.position.y < 0) return Error("스폰 포인트의 y좌표는 0 이상이어야 합니다.");
            
            return base.ValidationCheck();
        }
#endif
    }
}