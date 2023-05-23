using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public enum ComparisonOperator
    {
        [InspectorName("Equal to")] 
        EqualTo,
        [InspectorName("Not equal to")]
        NotEqualTo,
        [InspectorName("Greater than")]
        GreaterThan,
        [InspectorName("Less than")]
        LessThan,
        [InspectorName("Greater or equal")]
        GreaterThanEqual,
        [InspectorName("Less or equal")]
        LessThanEqual
    }
}