using UnityEngine.Events;
using WitchCompany.Toolkit.Module;

namespace WitchCompany.Toolkit.Runtime.Scripts.WitchBehaviours.Event.Base
{
    public abstract class WitchUIBase : WitchBehaviour
    {
        public class UIParam
        {
            public JRankCustomGetResponse rankData;
            public int rank = -1;
            public int reward;
            public char digit;
        }
    }
}