using System.ComponentModel;
using UnityEngine;

namespace WitchCompany.Toolkit.Runtime.Scripts.Enum
{
    public enum ComparisonOperator
    {
        [InspectorName("==")] 
        EqualTo,
        [InspectorName("!=")]
        NotEqualTo,
        [InspectorName(">")]
        GreaterThan,
        [InspectorName("<")]
        LessThan,
        [InspectorName(">=")]
        GreaterThanOrEqualTo,
        [InspectorName("<=")]
        LessThanEqualTo
    }
}