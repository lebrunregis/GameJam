using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayButtonPushed()
    {
        SceneManager.LoadScene("DevScene");
    }
    public void OnQuitButtonPushed()
    {
        Application.Quit();
    }

    public void OnPlayerDeath(bool val)
    {
        if (val)
        {
            Debug.Log("Player is alive");
        } else
        {
            Debug.Log("Player is dead");
        }
    }
}
