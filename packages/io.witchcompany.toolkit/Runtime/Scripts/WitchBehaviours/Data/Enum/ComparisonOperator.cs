using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public enum ComparisonOperator
    {
        [InspectorName("is")] 
        EqualTo,
        [InspectorName("is not")]
        NotEqualTo,
        [InspectorName("bigger than")]
        GreaterThan,
        [InspectorName("smaller than")]
        LessThan,
        [InspectorName("equal or bigger than")]
        GreaterThanOrEqualTo,
        [InspectorName("equal or smaller than")]
        LessThanEqualTo
    }
}