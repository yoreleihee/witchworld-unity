﻿using System.Collections.Generic;

namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JUnityKeyData
    {
        public string pathName;
        public string theme;
        public int capacity;
        public int isOfficial;
        public List<JUnityKeyDetail> unityKeyDetail;
        public bool isPrivate;
        public JRankingKey gameUnityKey;
    }
}