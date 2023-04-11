namespace WitchCompany.Toolkit.Validation
{
    public static class ValidationTag
    {
        public const string Optimization = "optimization";
        public const string Script = "script";
        public const string TagMissingScript = "Missing Script";
        
        // 최적화 검사
        public const string TagMeshVertex = "Mesh Vertex";
        public const string TagMaterial = "Material";
        public const string TagTexture = "Texture";
        public const string TagLightmap = "Light Map";
        public const string TagMeshCollider = "Mesh Collider";
        public const string TagRealtimeLight = "Realtime Light ";
        public const string TagReflectionProbe = "Reflection Probe";
        
        // 유효성 검사
        public const string TagComponent = "Component";
        public const string TagBadObject = "Bad Object";
        public const string TagBatchingStatic = "Batching Static";
    }
}