using System;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.UI
{
    public class WitchSyncedTextHandlerUI : WitchUIBase
    {
        public override string BehaviourName => "UI: 텍스트 동기화 핸들러";
        public override string Description => "자식 오브젝트가 WitchSyncedTextUI 컴포넌트를 가지고 있다면 type에 맞는 값을 Text Mesh Pro의 text로 지정합니다.";
        public override string DocumentURL => "";

        [Header("랭킹"), SerializeField, ReadOnly]
        private WitchRankingBoard ranking;

        [Header("랭킹 Scroll View Content에 위치"),SerializeField] 
        private GameObject scrollViewCellPrefab;

        [SerializeField] private Transform content;

        [Header("최상단 총 상금"), SerializeField] 
        private WitchSyncedTextUIGroup totalPrizeUI;

        [Header("Winner, 1~3등"), SerializeField]
        private WitchSyncedTextUIGroup[] winnerUIGroups;
        // [Header("자식 오브젝트 Text UI"), SerializeField, ReadOnly]
        // private WitchSyncedTextUI[] scrollViewTexts;
        private UIParam param = new UIParam();
        

        public WitchRankingBoard Ranking => ranking;
        public GameObject ScrollViewCellPrefab => scrollViewCellPrefab;
        public WitchSyncedTextUIGroup TotalPrizeUI => totalPrizeUI;
        public WitchSyncedTextUIGroup[] WinnerUIGroups => winnerUIGroups;
        public UIParam Param => param;
        

        [HideInInspector] public UnityEvent<JRankCustomGetResponse[], int, int> addList = new();
        [HideInInspector] public UnityEvent<int> totalPrize = new();

        public void AddList(JRankCustomGetResponse[] list,int page, int multiple ) => addList.Invoke(list, page, multiple);
        public void TotalPrize(int prize) => totalPrize.Invoke(prize);

        private void FindRankingBoard()
        {
            if (Application.isPlaying) return;

            ranking = GetComponentInParent<WitchRankingBoard>();
            
            if (scrollViewCellPrefab == null)
            {
                scrollViewCellPrefab = Instantiate(Resources.Load<GameObject>("Prefab/" + "TextGroup"), content);
            }
        }

        private void OnValidate()
        {
            if(!Application.isPlaying) FindRankingBoard();
        }

        private void Reset()
        {
            if(!Application.isPlaying) FindRankingBoard();
        }

        public void SetParam(JRankCustomGetResponse data, int rank, int reward)
        {
            param.rankData = data;
            param.rank = rank;
            param.reward = reward;
        }
    }
}