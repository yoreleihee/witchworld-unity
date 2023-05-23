using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue(Button) - ", menuName = "WitchToolkit/Dialogue/Button")]
    public sealed class WitchDialogueButtonSO : WitchDialogueSOBase
    {
        [field: Header("선택지(최대 10개)")]
        [field: SerializeField] public List<ButtonData> Buttons { get; private set; }

        [System.Serializable]
        public class ButtonData
        {
            [Header("내용")]
            public string content;
            
            [Header("URL 오픈 여부")]
            public bool openUrl;
            public string url;
            
            [Header("다음 대화(없다면 대화 종료)")]
            public WitchDialogueSOBase nextDialogue;

            [Header("이벤트")] 
            public UnityEvent onClicked;
        }
        
#if UNITY_EDITOR
        public override void ValidationCheck(ref ValidationReport report)
        {
            base.ValidationCheck(ref report);

            if (Buttons.Count is <= 0 or > 10)
            {
                report.Append(Error($"'{name}'의 대화 버튼은 최소 하나, 최대 열개까지 가능합니다."));
                return;
            }

            foreach (var btn in Buttons)
            {
                if (string.IsNullOrEmpty(btn.content))
                    report.Append(Error($"{name}: 대화 버튼의 content를 설정해주세요."));
                if(btn.openUrl && string.IsNullOrEmpty(btn.url))
                    report.Append(Error($"{name}: 대화 버튼의 URL을 설정해주세요."));
                
                if(btn.nextDialogue != null)
                    btn.nextDialogue.ValidationCheck(ref report);
            }
        }
#endif
    }
}