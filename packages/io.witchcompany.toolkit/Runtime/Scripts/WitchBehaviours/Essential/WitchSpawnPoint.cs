namespace WitchCompany.Toolkit.Module
{
    public class WitchSpawnPoint : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 스폰 포인트";
        public override string Description => "플레이어의 시작점이 되는 지점입니다. 한 씬에 하나만 배치할 수 있습니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;
    }
}