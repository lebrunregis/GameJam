using UnityEngine;
using UnityEngine.Events;


public class SyncedIntTMPListener : SyncedValueListener<int>
{
    public UnityEvent<string> _raisedString;
    public void UpdateCurrentScore(int score)
    {
      //  UIScore = score;
    }

    public new void OnRaiseEvent(int val)
    {
        OnRaiseEvent(val.ToString());
        base.OnRaiseEvent(val);
    }

    public void OnRaiseEvent(string val)
    {
        _raisedString.Invoke(val);
    }
}
