using UnityEngine;

namespace WitchCompany.Toolkit.Runtime.Scripts
{
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