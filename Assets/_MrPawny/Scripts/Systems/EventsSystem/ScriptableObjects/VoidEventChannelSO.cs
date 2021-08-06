namespace Pawny.Systems.EventSystem
{
    [UnityEngine.CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO: EventChannelBaseSO
    {
        public VoidAction OnEventRaised;

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }

    public delegate void VoidAction();
}
