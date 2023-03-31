namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class OptimizationConfig
    {
        private const int K = 1000;
        
        // // Test
        public const uint MAX_INDIVIDUAL_VERTS = 0 * K;
        public const uint MAX_VERTS = 0 * K;
        
        // 임시
        public const uint MAX_UNIQUE_MATERIALS = 0;
        public const uint MAX_SHARED_TEXTURE_MB = 0;
        public const uint MAX_LIGHTMAP_MB = 0;
        public const uint MAX_REFLECTION_PROBE_MB = 0;
        
        // TEST
        // public const uint MAX_INDIVIDUAL_VERTS = 16 * K;
        // public const uint MAX_VERTS = 256 * K;
        //
        // public const uint MAX_UNIQUE_MATERIALS = 75;
        // public const uint MAX_SHARED_TEXTURE_MB = 200;
        // public const uint MAX_LIGHTMAP_MB = 200;
        // public const uint MAX_REFLECTION_PROBE_MB = 200;


        public const string TAG_MESH_VERTEX = "Mesh Vertex";
        public const string TAG_MATERIAL = "Material";
        public const string TAG_TEXTURE = "Texture";
        public const string TAG_LIGHTMAP = "Light Map";
        public const string TAG_MESH_COLLIDER = "Mesh Collider";
        public const string TAG_LIGHTMODE_REALTIME = "Light Mode [Realtime]";
        public const string TAG_REFLECTION_PROBE = "Reflection Probe";



    }
    
}