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
        public ValidationReport ValidationCheck()
        {
            if(Buttons.Count is <= 0 or > 10)
                return new ValidationReport().Append($"{name}: 대화 버튼은 최소 하나, 최대 열개까지 가능합니다.", ValidationTag.TagDialogue, this);

            var report = new ValidationReport();
            
            foreach (var btn in Buttons)
            {
                if (string.IsNullOrEmpty(btn.content))
                    report.Append($"{name}: 대화 버튼의 내용이 비었습니다.", ValidationTag.TagDialogue, this);
                if(btn.openUrl && string.IsNullOrEmpty(btn.url))
                    report.Append($"{name}: 대화 버튼의 URL이 잘못되었습니다.", ValidationTag.TagDialogue, this);
            }

            return report;
        }
#endif
    }
}