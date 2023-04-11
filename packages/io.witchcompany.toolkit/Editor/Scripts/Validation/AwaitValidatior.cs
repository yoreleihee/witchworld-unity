using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchCompany.Toolkit.Editor.API;
using WitchCompany.Toolkit.Editor.DataStructure;
using WitchCompany.Toolkit.Runtime;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Editor.Validation
{
    public class AwaitValidator
    {
        public static async UniTask<ValidationReport> ValidationCheck()
        {
            var scene = SceneManager.GetActiveScene();

            return new ValidationReport()
                .Append(await ValidatePrivateItem());
        }
        
        // validation check 시 api 호출해야 함
        // await로 결과 기다려야 함
        // validation check도 비동기 함수로 바꿔야 하는가..??
        private static async UniTask<ValidationReport> ValidatePrivateItem()
        {
            var report = new ValidationReport();
            
            var privateDoors = GameObject.FindObjectsOfType<WitchPrivateDoor>(true);

            foreach (var door in privateDoors)
            {
                var item = await WitchAPI.GetValidItem(door.ItemKey);

                if (item == false)
                {
                    var error = new ValidationError($"Object : {door.gameObject.name}\n" +
                                                    $"Witch Private Door의 item key({door.ItemKey})로 등록된 상품이 없습니다.", ValidationTag.Script, door);
                    report.Append(error);
                }
            }
            return report;
        }
    }
}