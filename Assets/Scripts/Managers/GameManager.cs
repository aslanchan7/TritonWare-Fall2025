using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPlaying;
    public double AudioStartTime;

    void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void EndGame()
    {
        ResultsManager.Instance.ShowResultsMenu();
        IsPlaying = false;
        AudioManager.Instance.StopMusic();
    }

    public void StartGame()
    {
        IsPlaying = true;
        AudioManager.Instance.PlayMusic();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }
}
