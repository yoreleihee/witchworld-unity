using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit
{
    public class WitchRewardCheck : WitchBehaviourUnique
    {
        public override string BehaviourName => "리워드 체크 + 지급";

        public override string Description => "리워드 체크 및 지급 할 수 있는 요소입니다.\n" +
                                              "ItemNumber에 리워드로 사용할 아이템 번호를 입력해주세요";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        [field: Header("임시 바이프로스트, 바프 블록이면 체크")]
        [field: SerializeField] public bool isBifrost { get; private set; }
        
        [field: Header("리워드 아이템 넘버")]
        [field: SerializeField] public string itemNumber { get; private set; }

        //[HideInInspector] public UnityEvent rewardCheckEvent = new();
        //[HideInInspector] public UnityEvent rewardPaymentEvent = new();
        [HideInInspector] public UnityEvent rewardCheckAndPaymentEvent = new();
        [HideInInspector] public UnityEvent bifrostRewardCheckAndPaymentEvent = new();

        //public void RewardCheck() => rewardCheckEvent.Invoke();
        //public void RewardPayment() => rewardPaymentEvent.Invoke();
        public void RewardCheckAndPayment() => rewardCheckAndPaymentEvent.Invoke();
        public void BifrostRewardCheckAndPayment() => bifrostRewardCheckAndPaymentEvent.Invoke();
    }
}
