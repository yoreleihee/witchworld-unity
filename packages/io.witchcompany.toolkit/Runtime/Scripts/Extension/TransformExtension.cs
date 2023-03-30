using System.Linq;
using UnityEngine;

namespace WitchCompany.Toolkit.Extension
{
    public static class TransformExtension
    {
        public static bool HasChild<T>(this Transform target, T child, bool includeSelf = true) where T : Component
        {
            if(includeSelf)
                if (target.gameObject == child.gameObject) return true;
            
            var comps = target.GetComponentsInChildren<T>();
            return comps.Any(x => x.gameObject == child.gameObject);
        }
    }
}