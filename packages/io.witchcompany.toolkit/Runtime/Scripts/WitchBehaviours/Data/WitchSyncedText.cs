using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    [DisallowMultipleComponent]
    public class WitchSyncedText : WitchDataBase
    {
        public override string BehaviourName => "데이터 통신: 텍스트 값 동기화";

        public override string Description => "Key에 해당하는 값을 읽어와\n" +
                                              "해당 값을 텍스트에 보여주는 기능입니다.\n" +
                                              "TMP_Text, Text, TextMesh 중 하나가 필요합니다.\n" +
                                              "타이머의 키값은 @timer 입니다.\n\n" +
                                              "*데이터 매니저가 필요합니다.";

        public override string DocumentURL => "https://www.notion.so/witchcompany/WitchSyncedText-8a340f9fdfda4763ac78afccdcfeba6a?pvs=4";
        
        [Header("읽어올 Key값")]
        [SerializeField] private string key = "key";
        public string Key => key;

#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (string.IsNullOrEmpty(key))
                return NullError("key");

            var count = 0;
            if (TryGetComponent<TMP_Text>(out _)) count++;
            if (TryGetComponent<Text>(out _)) count++;
            if (TryGetComponent<TextMesh>(out _)) count++;

            if (count == 0)
                return Error("TMP_Text, Text, TextMesh 중 하나가 필요합니다.");
            if (count != 1)
                return Error("TMP_Text, Text, TextMesh 중 하나만 가질 수 있습니다.");
            
            return base.ValidationCheck();
        }
#endif
    }
}