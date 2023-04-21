using UnityEngine;

namespace WitchCompany.Core
{
    public static class CustomGizmo
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void DrawArrow(Vector3 start, Vector3 end, Color color, float radius = 0.25f)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);

            Gizmos.DrawRay(
                end,
                Quaternion.AngleAxis(45, Vector3.forward) * Vector3.Normalize(start - end) * radius
            );

            Gizmos.DrawRay(
                end,
                Quaternion.AngleAxis(-45, Vector3.forward) * Vector3.Normalize(start - end) * radius
            );
        }   
    }
}