namespace WitchCompany.Toolkit.Editor.DataStructure
{
    [System.Serializable]
    public class JBuildGroup
    {
        public string target;
        public string exportPath;
        public long totalSizeByte;

        // public JBuildGroup(string target, string path, long size)
        // {
        //     this.target = target;
        //     exportPath = path;
        //     totalSizeByte = size;
        // }
    }
}