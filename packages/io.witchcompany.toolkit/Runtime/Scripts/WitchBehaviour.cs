using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchCompany.Toolkit
{
    public abstract class WitchBehaviour : MonoBehaviour
    {
        public abstract string BehaviourName { get; }
        public abstract string Description { get; }
        public abstract string DocumentURL { get; }

    }
}
