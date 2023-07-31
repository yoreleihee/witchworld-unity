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
        LeftArm = 1 << 5,
        LeftForeArm = 1 << 6,
        LeftWrist = 1 << 7,
        LeftHand = 1 << 8,
        LeftUpLeg = 1 << 9,
        LeftLeg = 1 << 10,
        LeftAnkle = 1 << 11,
        LeftFoot = 1 << 12,
        RightArm = 1 << 13,
        RightForeArm = 1 << 14,
        RightWrist = 1 << 15,
        RightHand = 1 << 16,
        RightUpLeg = 1 << 17,
        RightLeg = 1 << 18,
        RightAnkle = 1 << 19,
        RightFoot = 1 << 20,
    }
}