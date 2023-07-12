namespace WitchCompany.Toolkit
{
    [System.Flags]
    public enum SkinType
    {
        None = 0,
        Everything = ~0,
        Head = 1 << 1,
        Neck = 1 << 2,
        Top = 1 << 3,
        Bottom = 1 << 4,
        Arm = 1 << 5,
        ForeArm = 1 << 6,
        Hand = 1 << 7,
        UpLeg = 1 << 8,
        Leg = 1 << 9,
        Foot = 1 << 10,
    }
}