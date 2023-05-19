using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public enum ArithmeticOperator
    {
        [InspectorName("+")] 
        Add,
        [InspectorName("x")] 
        Multiply,
        [InspectorName("÷")]
        Divide,
        [InspectorName("=")]
        Assignment
    }
}