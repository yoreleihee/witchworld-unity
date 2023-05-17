using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Module;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base;
using WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.UI;

namespace WitchCompany.Toolkit.Runtime
{
    public class WitchRankingBoard : WitchUIBase
    {
        public override string BehaviourName => "블록 랭킹 보드";
        public override string Description => "블록의 랭킹을 나타냅니다.\n" +
                                              "블록의 랭킹에 필요한 키 값을 넣어 랭킹을 불러옵니다.\n" +
                                              "WitchDataManager가 같은 씬에 있어야 합니다.\n";
        public override string DocumentURL => "";

        [Header("읽기전용"),SerializeField, ReadOnly] 
        private WitchDataManager dataManager;

        [Header("랭킹 표시할 오브젝트"),SerializeField]
        private WitchSyncedTextHandlerUI rankingCard;

        
        
        [Header("불러올 랭킹 키 값"),SerializeField] 
        private string[] rankKeys;
        
        [Header("읽기전용\n현재 랭킹 페이지, 0페이지 부터 시작"),SerializeField] 
        private int currentPage = 0;
        
        [Header("랭킹 페이지 당 보여줄 player 수\n0 입력시 한번에 다 불러오기"),SerializeField]
        private int playerPerPage;

        [Header("순위별 보상, default 보상"), SerializeField]
        private int totalPrize;
        [SerializeField] private int[] rewards;
        [SerializeField] private int defaultReward;

        [Header("다른 블록 정보 가져오기"), SerializeField]
        private bool customRanking;
        
        [Header("조회할 블록 이름\nCustomRanking이 체크 되어 있어야 합니다.")]
        [SerializeField] private string pathName;
        [SerializeField, ReadOnly] private int blockId;

        //랭킹 데이터 수신 상태
        private DataState rankDataState;
        
        public WitchDataManager DataManager => dataManager;
        public WitchSyncedTextHandlerUI RankingCard => rankingCard;
        
        public string[] RankKeys => rankKeys;
        public int CurrentPage => currentPage;
        public int PlayerPerPage => playerPerPage;
        public int TotalPrize => totalPrize;
        public int[] Rewards => rewards;
        public int DefaultReward => defaultReward;
        public bool CustomRanking => customRanking;
        public string PathName => pathName;
        public int BlockId => blockId;
        public DataState RankDataState => rankDataState;
        

        [HideInInspector] public UnityEvent<string[], int, int> getRank = new();
        [HideInInspector] public UnityEvent<OperatorType, int> movePage = new();

        //toolkit 내부에서 랭킹 데이터 얻기, 페이징
        public async UniTask GetRank(string[] keys, int page, int limit)
        {
            rankDataState = DataState.Nothing;
            
            getRank.Invoke(keys, page, limit);

            try
            {
                //키값 데이터 통신 완료까지 대기, timeout 10초
                await UniTask.WaitUntil(() => rankDataState == DataState.Complete).Timeout(TimeSpan.FromSeconds(10));
            }
            catch
            {
                Debug.LogError("get rank failed...");
            }
        }
        
        //랭킹보드 페이지 이동
        public void MovePage(OperatorType oper) => movePage.Invoke(oper, currentPage);
        
        //WitchDataManager 찾기
        private void FindDataManager()
        {
            if (Application.isPlaying) return;

            dataManager = transform.root.GetComponentInChildren<WitchDataManager>(true);
            rankingCard = GetComponentInChildren<WitchSyncedTextHandlerUI>(true);
            
            if(rankingCard == null) CreateRankingObject();

            if(dataManager == null) Debug.LogWarning("WitchDataManager가 없습니다!!!\n RankingBoard를 사용하려면 WitchDataManager가 필요합니다!!!");
        }

        private void OnValidate()
        {
            if(!Application.isPlaying) FindDataManager();
        }

        private void Reset()
        {
            if(!Application.isPlaying) FindDataManager();
        }
        
        //데이터 수신 상태 업데이트
        public void RankDataStateUpdate(DataState state)
        {
            rankDataState = state;
        }

        public void SetBlockId(int id)
        {
            Debug.LogWarning($"block id :: {id}");
            blockId = id;
        }

        private void CreateRankingObject()
        {
            Instantiate(Resources.Load("Prefab/" + "Canvas-RankingBoard"), transform);
            FindDataManager();
        }
    }
}
