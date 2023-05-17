using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchSyncedTextUIGroup : WitchUIBase
    {
        public override string BehaviourName => "UI Group: 동기화 텍스트 1단위 묶음";

        public override string Description => "랭킹 하나의 셀 묶음 입니다.\n" +
                                              "하위에 WitchSyncedTextUI를 두고 key값을 넣어주면 key에 해당하는 Text가 UI에 나타납니다.";
        public override string DocumentURL => "";

        //그룹에 쓰일 랭킹 데이터
        [SerializeField, ReadOnly] private JRankCustomGetResponse cellData;
        [SerializeField, ReadOnly] private List<WitchSyncedTextUI> texts;
        [SerializeField, ReadOnly] private List<WitchSyncedTextUI> fixedTexts;
        
        public JRankCustomGetResponse CellData => cellData;
        public List<WitchSyncedTextUI> Texts => texts;
        public List<WitchSyncedTextUI> FixedTexts => fixedTexts;

        //[HideInInspector] public UnityEvent<JRankCustomGetResponse, int> setRankData = new();
        [HideInInspector] public UnityEvent<UIParam> setRankData = new();
        [HideInInspector] public UnityEvent<UIParam,int> totalPrize = new();

        //public void SetRankData(JRankCustomGetResponse data,int rank) => setRankData.Invoke(data, rank);
        public void SetRankData(UIParam param) => setRankData.Invoke(param);
        public void TotalPrize(UIParam param,int index) => totalPrize.Invoke(param, index);

        private void FindTextUI()
        {
            if (Application.isPlaying) return;

            texts = GetComponentsInChildren<WitchSyncedTextUI>(true).ToList();
            fixedTexts.Clear();
            
            foreach (var text in texts)
            {
                if (text.UiType == UIStandardType.FixedText)
                {
                    fixedTexts.Add(text);
                }
            }

            foreach (var text in fixedTexts)
            {
                texts.Remove(text);
            }
        }

        private void OnValidate()
        {
            if(!Application.isPlaying) FindTextUI();
        }

        private void Reset()
        {
            if(!Application.isPlaying) FindTextUI();
        }
    }
}
