using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public abstract class WitchDataBase : WitchBehaviour
    {
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (FindObjectOfType<WitchDataManager>() == null)
                return Error("WitchDataManager가 필요합니다!");

            return base.ValidationCheck();
        }
#endif
    }
}