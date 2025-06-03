using UnityEngine;
using UnityEngine.Events;

public class SyncedValueListener<T> : MonoBehaviour
{
    [SerializeField]
    public SyncedValue<T> m_syncValue;
    [SerializeField]
    public UnityEvent<T> m_raisedEvent;

    private void OnEnable()
    {
        m_raisedEvent.Invoke(m_syncValue.Subscribe(m_raisedEvent));
    }

    private void OnDisable()
    {
        m_syncValue.Unsubscribe(m_raisedEvent);
    }

    public void OnRaiseEvent(T val)
    {
        m_raisedEvent.Invoke(val);
    }
}
