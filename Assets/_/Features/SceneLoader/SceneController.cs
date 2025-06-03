using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    bool m_gameIsOver = false;
    public void OnPlayButtonPushed()
    {
        SceneManager.LoadScene("DesignScene",LoadSceneMode.Single);
    }
    public void OnQuitButtonPushed()
    {
        Application.Quit();
    }

    public void OnPlayerAlive(bool isAlive)
    {
        if (!isAlive && !m_gameIsOver)
        {
            Debug.Log("Player is dead");
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
            m_gameIsOver = true;
        }
    }
}
