namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class OptimizationConfig
    {
        private const int K = 1000;
        
        // // Test
        public const uint MaxIndividualVerts = 0 * K;
        public const uint MaxVerts = 0 * K;
        
        // 임시
        public const uint MaxUniqueMaterials = 0;
        public const uint MaxSharedTextureMb = 0;
        public const uint MaxLightmapMb = 0;
        public const uint MaxReflectionProbeMb = 0;
        
        // TEST
        // public const uint MaxIndividualVerts = 16 * K;
        // public const uint MaxVerts = 256 * K;
        //
        // public const uint MaxUniqueMaterials = 75;
        // public const uint MaxSharedTextureMb = 200;
        // public const uint MaxLightmapMb = 200;
        // public const uint MaxReflectionProbeMb = 200;


        public const string TagMeshVertex = "Mesh Vertex";
        public const string TagMaterial = "Material";
        public const string TagTexture = "Texture";
        public const string TagLightmap = "Light Map";
        public const string TagMeshCollider = "Mesh Collider";
        public const string TagRealtimeLight = "Realtime Light ";
        public const string TagReflectionProbe = "Reflection Probe";



    }
    
}