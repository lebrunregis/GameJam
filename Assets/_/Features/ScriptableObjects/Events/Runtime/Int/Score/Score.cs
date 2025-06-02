using UnityEngine;

public class Score : MonoBehaviour
{
    public SyncedValue<int> currentScore;

    public void IncreaseScore(int score)
    {
        currentScore.SourceValue += score;
    }
}
