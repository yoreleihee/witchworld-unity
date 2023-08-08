namespace WitchCompany.Toolkit
{
    [System.Flags]
    public enum SkinType
    {
        None = 0,
        Everything = ~0,
        Head = 1 << 0,
        Neck = 1 << 1,
        Top = 1 << 2,
        Bottom = 1 << 3,
        LeftShoulder = 1 << 4,
        LeftArm = 1 << 5,
        LeftWrist = 1 << 6,
        LeftHand = 1 << 7,
        LeftUpLeg = 1 << 8,
        LeftLeg = 1 << 9,
        LeftAnkle = 1 << 10,
        LeftFoot = 1 << 11,
        RightShoulder = 1 << 12,
        RightArm = 1 << 13,
        RightWrist = 1 << 14,
        RightHand = 1 << 15,
        RightUpLeg = 1 << 16,
        RightLeg = 1 << 17,
        RightAnkle = 1 << 18,
        RightFoot = 1 << 19,
    }
}