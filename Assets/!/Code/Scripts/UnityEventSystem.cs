using UnityEngine;
using UnityEngine.Events;

namespace UnityEventsNS
{
    public class UnityEventSystem : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent Events;

        public void InvokeEvent() => Events?.Invoke();
    }
}
