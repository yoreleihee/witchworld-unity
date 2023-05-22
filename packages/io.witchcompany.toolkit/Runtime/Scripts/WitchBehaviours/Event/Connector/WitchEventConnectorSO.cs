using UnityEngine;
using UnityEngine.Events;

namespace WitchCompany.Toolkit.Module.Event
{
    [CreateAssetMenu(fileName = "EventConnector - ", menuName = "WitchToolkit/EventConnector")]
    public class WitchEventConnectorSO : ScriptableObject
    {
        [HideInInspector] public UnityEvent onInvoke;
        
        public void Invoke() => onInvoke.Invoke();
    }
}