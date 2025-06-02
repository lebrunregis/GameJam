using UnityEngine;
using UnityEngine.Events;

public class SyncedValueListener<T> : MonoBehaviour
{
    [SerializeField]
    public SyncedValue<T> syncValue;
    [SerializeField]
    public UnityEvent<T> _raised;
    [SerializeField]

    private void OnEnable()
    {
        _raised.Invoke(syncValue.Subscribe(_raised));
    }

    private void OnDisable()
    {
        syncValue.Unsubscribe(_raised);
    }

    public void OnRaiseEvent(T val)
    {
        _raised.Invoke(val);
    }
}
