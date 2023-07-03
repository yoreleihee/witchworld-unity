using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WitchCompany.Toolkit.Validation;

namespace WitchCompany.Toolkit.Module
{
    public class WitchRopeJumpGame : WitchBehaviour
    {
        public override string BehaviourName => "게임: 줄넘기";
        public override string Description => "즐거운 줄넘기~\n" +
                                              "1. Humanoid로 리깅된 NPC가 2마리 필요합니다. 손 끝에 ChainIK를 달아주세요..\n" +
                                              "1-2. NPC의 애니메이터는 int형으로 구성된 parameter가 있어야 합니다.\n" +
                                              "2. FaceMats, SkyBoxTints는 는 4단계로 이루어져야 합니다. 노말, 빠름, 매우빠름, 미침\n" +
                                              "3. Rope는 전체 길이기 4여야 합니다.\n" +
                                              "4. ScoreText는 현재 점수가, StateText는 게임 진행상황이 표시됩니다.\n" +
                                              "5. JoinGameEffect는 게임 참가시, OutGameEffect는 줄에 걸릴 시 플레이됩니다.";
        public override string DocumentURL => "";
        public override int MaximumCount => 1;

        public const float TurningRadius = 0.15f;
        public const float RopeHeight = 1f;
        public const float HalfLenght = 4f;

        [field: Header("NPC")]
        [field: SerializeField] public Npc[] Npcs { get; private set; }
        [field: SerializeField] public List<Material> FaceMats { get; private set; }
        
        [field: Header("Rope")]
        [field: SerializeField] public Transform Rope { get; private set; }

        [field: Header("UI")]
        [field: SerializeField] public TMP_Text ScoreText { get; private set; }
        
        [field: Header("효과")]
        [field: SerializeField] public ParticleSystem JoinGameEffect { get; private set; }
        [field: SerializeField] public ParticleSystem JumpFailEffect { get; private set; }
        [field: SerializeField] public RopeJumpComboEffect ComboEffects { get; private set; }
        [field: SerializeField] public RopeJumpMessageData MessageData { get; private set; }
        [field: SerializeField] public List<Material> SkyBoxMats { get; private set; }
        
        [field: Header("SE(필수 아님)")]
        [field: SerializeField] public AudioClip JoinGameSE { get; private set; }
        [field: SerializeField] public AudioClip StartGameSE { get; private set; }
        [field: SerializeField] public AudioClip JumpFailSE { get; private set; }
        [field: SerializeField] public AudioClip JumpSuccessSE { get; private set; }
        [field: SerializeField] public AudioClip ReverseChichiSE { get; private set; }
        [field: SerializeField] public AudioClip AngryChichiSE { get; private set; }
        [field: SerializeField] public AudioClip CrazyChichiSE { get; private set; }
        [field: SerializeField] public AudioClip ComboSE { get; private set; }

        [Serializable]
        public class Npc
        {
            public Animator animator;
            public SkinnedMeshRenderer meshRenderer;
            public Transform handle;
        }
        
#if UNITY_EDITOR
        public override ValidationError ValidationCheck()
        {
            if (transform.rotation != Quaternion.identity) return Error("회전값은 0이어야 합니다.");
            
            if (ScoreText == null) return NullError(nameof(ScoreText));

            if (FaceMats.Count != 4) return Error("FaceMats 는 4단계로 이루어져야 합니다. 노말, 빠름, 매우빠름, 미침");
            //if (SkyBoxTints.Length != 3) return Error("SkyBoxTints는 3단계로 이루어져야 합니다. 노말, 빠름, 매우빠름");

            if (Rope == null)
                return NullError(nameof(Rope));
            
            if (Npcs.Length != 2)
                return Error("NPC는 좌, 우 2마리 배치해야 합니다.");
            
            foreach (var npc in Npcs)
            {
                if (npc.animator == null) return NullError(nameof(npc.animator));
                if (npc.meshRenderer == null) return NullError(nameof(npc.meshRenderer));
                if (npc.handle == null) return NullError(nameof(npc.handle));
            }

            if (JoinGameEffect != null && JoinGameEffect.main.stopAction != ParticleSystemStopAction.Disable)
                return Error("JoinGameEffect의 StopAction을 Disable로 설정해주세요.");
            if (JumpFailEffect != null && JumpFailEffect.main.stopAction != ParticleSystemStopAction.Disable)
                return Error("OutGameEffect의 StopAction을 Disable로 설정해주세요.");

            return base.ValidationCheck();
        }
        
        private void OnDrawGizmos()
        {
            var p = transform.position;
            var left = p+new Vector3(-4, 1, 0);
            var leftFloor = p+new Vector3(-2, 0, 0);
            var right = p+new Vector3(4, 1, 0);
            var rightFloor = p+new Vector3(2, 0, 0);
            var width = Vector3.back * TurningRadius;
            var height = Vector3.up * TurningRadius;
            const float radius = 0.05f;
            
            Gizmos.color = Color.red;
            
            Gizmos.DrawSphere(left, radius);
            Gizmos.DrawLine(left, leftFloor);
            
            Gizmos.DrawSphere(right, radius);
            Gizmos.DrawLine(right, rightFloor);
            
            Gizmos.DrawLine(leftFloor, rightFloor);


            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawLine(left - height, left + height);
            Gizmos.DrawLine(left - width, left + width);
            Gizmos.DrawLine(right - height, right + height);
            Gizmos.DrawLine(right - width, right + width);
        }

        private void OnValidate()
        {
            if(ScoreText) ScoreText.text = "000";
        }
#endif
    }
}