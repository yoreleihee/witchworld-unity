using UnityEngine;
using WitchCompany.Toolkit.Module.PhysicsEffect;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
    public class Data
    {
        public string key;
        public int value;
        public GameObject go;
    }
    
    [CreateAssetMenu(fileName = "SetValueData", menuName = "Scriptable Object/SetValueData", order = int.MaxValue)]
    public class SetValueData : ScriptableObject
    {
        public Owner owner;
        public string key;
        public int value;
        public Operator oper;

        public enum Owner
        {
            User,
            Global
        }
        
        public enum Operator
        {
            Add,
            Sub,
            Multi,
            Div,
            Assignment
        }
    }
}