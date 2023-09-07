using UnityEngine;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit
{
    public class WitchBlockAssetPoint : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 블록 기본 에셋 포인트";
        public override string Description => "블록 기본 에셋이 배치될 지점을 정하는 요소입니다. 한 씬에 하나만 배치할 수 있습니다";
        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchBlockAssetPoint-38a2f3f0c8dd4626ad1bd6a40ad7e0e2?pvs=4";
        public override int MaximumCount => 1;
        
    }
}
