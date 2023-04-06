using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.Tool;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public static class ObjectValidator
    {
        public static ValidationReport ValidationCheck()
        {
            return new ValidationReport()
                .Append(ValidateObjectID());
        }

        private static ValidationReport ValidateObjectID()
        {
            var report = new ValidationReport();
            
            foreach (var tr in GetAllTransforms())
            {
                // 게임오브젝트 가져오기
                var go = tr.gameObject;
                // 해당 오브젝트의 id 가져오기
                var id = ObjectTool.GetLocalIdentifier(go);

                if (id == 0)
                    report.Append($"비정상적인 오브젝트가 존재합니다: {go.name}", ValidationTag.TagBadObject, go);
            }

            return report;
        }
        
        // 현재 씬의 모든 오브젝트 가져오기
        private static List<Transform> GetAllTransforms()
        {
            var scn = SceneManager.GetActiveScene();
            var parent = scn.GetRootGameObjects()[0];
            return HierarchyTool.GetAllChildren(parent.transform);
        }
    }
}