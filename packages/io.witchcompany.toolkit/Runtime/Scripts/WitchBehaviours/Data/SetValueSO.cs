using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Logic
{
    [CreateAssetMenu(fileName = "SetValue(SO) - ", menuName = "WitchCompany/Data/SetValueSO")]
    public class SetValueSO : ScriptableObject
    {
        public string key;
        public string value;
    }
}
