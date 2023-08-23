namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class OptimizationConfig
    {
        private const uint K = 1000;
        
        public const uint MaxIndividualVerts = 16 * K;
        public const uint MaxVerts = 80 * K;
        public const uint MaxMeshColliderObject = 1;
        
        public const uint MaxUniqueMaterials = 80;
        public const uint MaxSharedTextureMb = 35;
        public const uint MaxDirectionalLight = 1;
        public const uint MaxRealtimeLight = 2;
        public const uint MaxLightmapMb = 200;
        public const uint MaxAudioClipMb = 20;
        public const uint MaxReflectionProbeMb = 200;

        public const uint MaxMeshColliderCount = 2;
    }
    
}