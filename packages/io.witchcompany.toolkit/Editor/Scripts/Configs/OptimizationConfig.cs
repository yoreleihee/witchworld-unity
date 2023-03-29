namespace WitchCompany.Toolkit.Editor.Configs
{
    public static class OptimizationConfig
    {
        private const int K = 1000;
        
        public const uint MAX_INDIVIDUAL_VERTS = 16 * K;
        public const uint MAX_VERTS = 256 * K;
        
        // 임시
        public const uint MAX_UNIQUE_MATERIALS = 75;
        public const uint MAX_SHARED_TEXTURE_MB = 200;
        public const uint MAX_LIGHTMAP_MB = 200;
        
        // // Test
        // public const uint MAX_INDIVIDUAL_VERTS = 0 * K;
        // public const uint MAX_VERTS = 0 * K;
        //
        // // 임시
        // public const uint MAX_UNIQUE_MATERIALS = 0;
        // public const uint MAX_SHARED_TEXTURE_MB = 0;
        // public const uint MAX_LIGHTMAP_MB = 0;
        
        // public const uint MaxVertexCountIndividual = 16 * 1000; // 16K
        // public const uint MaxVertexCountSum = 256 * 1000; // 500K
    }
    
}