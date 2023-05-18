using System.ComponentModel;
using UnityEngine;

namespace WitchCompany.Toolkit.Module
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