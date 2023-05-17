using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using WitchCompany.Core;
using WitchCompany.Logic;
using WitchCompany.Toolkit.Attribute;
using WitchCompany.Toolkit.Runtime;
using WitchCompany.Toolkit.Runtime.Scripts.Enum;

namespace WitchCompany.Toolkit.Module
{
    public class WitchDataManager : WitchBehaviourUnique
    {
        public override string BehaviourName => "필수: 데이터 매니저";
        public override string Description => "데이터를 읽고 쓸 수 있는 요소입니다.\n" +
                                              "데이터를 변경할 수 있는 기능을 제공합니다.\n" +
                                              //"블록 매니저와 같은 오브젝트에 배치되어야 합니다.\n" +
                                              "한 씬에 하나만 배치할 수 있습니다.\n";
        public override string DocumentURL => "";

        public override int MaximumCount => 1;

        #region Data List & State
        
        [Header("읽기전용"),SerializeField, ReadOnly]
        private WitchObjectActivator[] activators;

        //dataKey 별로 value값 저장
        private Dictionary<string, string> keyValues = new Dictionary<string, string>();
        //data 수신 상태
        private Dictionary<string, DataState> keyDataStates = new Dictionary<string, DataState>();
        //dataKey 별로 condition 상태 저장
        private Dictionary<string, bool> keyConditions = new Dictionary<string, bool>();
        //dataKey 별로 activator 저장, setValue 후 conditionCheck 및 activator 활성화에 필요 
        private Dictionary<string, WitchObjectActivator> keyActivators = new Dictionary<string, WitchObjectActivator>();
        //랭킹, int => ranking page
        private Dictionary<int,JRankCustomGetResponse[]> rankList = new Dictionary<int, JRankCustomGetResponse[]>();

        public Dictionary<string, WitchObjectActivator> KeyActivators => keyActivators;
        public WitchObjectActivator[] Activators => activators;
        public Dictionary<string, string> KeyValues => keyValues;
        public Dictionary<string, DataState> KeyDataStates => keyDataStates;
        public Dictionary<string, bool> KeyConditions => keyConditions;
        public Dictionary<int,JRankCustomGetResponse[]> RankList => rankList;

        #endregion

        #region Unity Events

        [HideInInspector] public UnityEvent<string> getValue = new();
        [HideInInspector] public UnityEvent<SetValueSO> setValue = new();
        
        [HideInInspector] public UnityEvent<WitchObjectActivator> getActivateValues = new();
        [HideInInspector] public UnityEvent<string> getActivateValue = new();
        [HideInInspector] public UnityEvent<SetValueSO> setActivateValue = new();

        //toolkit 내부에서 데이터 얻기
        public async UniTask<string> GetValue(string key)
        {
            //키값 데이터 통신 상태 nothing
            if (!keyDataStates.ContainsKey(key)) keyDataStates.Add(key, DataState.Nothing);
            else keyDataStates[key] = DataState.Nothing;

            //데이터 통신
            getValue.Invoke(key);
            
            try
            {
                //키값 데이터 통신 완료까지 대기, timeout 10초
                await UniTask.WaitUntil(() => keyDataStates[key] == DataState.Complete).Timeout(TimeSpan.FromSeconds(10));
                return keyValues[key];
            }
            catch
            {
                Debug.LogError("GetValue Failed.....!!");
                return null;
            }
        } 
        public void SetValue(SetValueSO data) => setValue.Invoke(data);
        
        public void GetActivateValues(WitchObjectActivator key) => getActivateValues.Invoke(key);
        public void GetActivateValue(string key) => getActivateValue.Invoke(key);
        public void SetActivateValue(SetValueSO data) => setActivateValue.Invoke(data);
        
        #endregion


        // WitchObjectActivator 전부 수집
        private void FindActivator()
        {
            if (Application.isPlaying) return;

            activators = transform.parent.GetComponentsInChildren<WitchObjectActivator>(true);
        }

        private void OnValidate()
        {
            if(!Application.isPlaying) FindActivator();
        }

        private void Reset()
        {
            if(!Application.isPlaying) FindActivator();
        }
    }
}