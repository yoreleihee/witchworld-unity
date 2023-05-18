using UnityEngine;

namespace WitchCompany.Toolkit.Module
{
    public enum ArithmeticOperator
    {
        [InspectorName("+")] 
        Add,
        [InspectorName("*")] 
        Multiply,
        [InspectorName("=")] 
        Assignment
    }
}