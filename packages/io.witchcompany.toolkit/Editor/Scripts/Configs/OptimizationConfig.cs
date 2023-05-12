namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class OptimizationConfig
    {
        private const int K = 1000;
        
        // // Test
        // public const uint MaxIndividualVerts = 0 * K;
        // public const uint MaxVerts = 0 * K;
        //
        // // 임시
        // public const uint MaxUniqueMaterials = 0;
        // public const uint MaxSharedTextureMb = 0;
        // public const uint MaxLightmapMb = 0;
        // public const uint MaxReflectionProbeMb = 0;
        
        
        public const uint MaxIndividualVerts = 16 * K;
        public const uint MaxVerts = 400 * K;
        public const uint MaxMeshColliderObject = 1;
        
        public const uint MaxUniqueMaterials = 80;
        public const uint MaxSharedTextureMb = 200;
        public const uint MaxDirectionalLight = 1;
        public const uint MaxPointLight = 2;
        public const uint MaxLightmapMb = 200;
        public const uint MaxReflectionProbeMb = 200;

        public const uint MaxMeshColliderCount = 2;
    }
    
}