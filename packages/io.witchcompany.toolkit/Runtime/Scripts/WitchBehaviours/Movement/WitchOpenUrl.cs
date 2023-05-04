using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchOpenUrl : WitchBehaviour
    {
        public override string BehaviourName => "이동: 새 탭에서 URL 열기";
        public override string Description => "지정한 url을 새 탭에서 열 수 있는 요소입니다.";
        public override string DocumentURL => "";

        [Header("새 탭에서 열 URL"), SerializeField]
        private string targetUrl;
        [HideInInspector]
        public UnityEvent openUrlEvent;
        
        public string TargetUrl => targetUrl;
        public void OpenUrl() => openUrlEvent.Invoke();
    }
}
