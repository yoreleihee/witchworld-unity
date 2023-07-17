namespace WitchCompany.Toolkit.Validation
{
    public static class ValidationTag
    {
        public const string TagOptimization = "optimization";
        public const string TagScript = "script";
        public const string TagMissingScript = "Missing Script";
        
        // 최적화 검사
        public const string TagMeshVertex = "Mesh Vertex";
        public const string TagMaterial = "Material";
        public const string TagTexture = "Texture";
        public const string TagLightmap = "Light Map";
        public const string TagMeshCollider = "Mesh Collider";
        public const string TagRigidbody = "Rigidbody";
        public const string TagDirectionalLight = "Directional";
        public const string TagRealtimeLight = "Point, Spot";
        public const string TagEtcRealtimeLight = "Realtime";
        public const string TagBaked = "Baked";
        public const string TagReflectionProbe = "Reflection Probe";
        
        // 유효성 검사
        public const string TagComponent = "Component";
        public const string TagBadObject = "Bad Object";
        public const string TagBatchingStatic = "Batching Static";
        
        // 필수 컴포넌트 검사
        public const string TagEssentialList = "Essential Component";
        
        // 상품 검사
        public const string TagProduct = "Product";
        
        // 기타
        public const string TagDialogue = "Dialogue";
        public const string TagPortal = "Portal";
    }
}