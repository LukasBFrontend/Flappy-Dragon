using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : Singleton<LogicScript>
{
    [HideInInspector]
    public int playerScore = 0;
    private GameObject scoreObject;
    private Text scoreText;

    [HideInInspector]
    public bool isGameOver = false;

    [HideInInspector]
    public bool isPaused = false;
    [HideInInspector]
    public bool isGameWon = false;

    void Start()
    {
        if (scoreText) scoreText.text = playerScore.ToString();

        if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            InvokeRepeating("TickingScore", 0f, 0.1f);
        }
    }
    public void AddScore(int points)
    {
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }

    public void TickingScore()
    {
        if (!isGameOver && !isPaused && SceneManager.GetActiveScene().name == "Lvl 1")
        {
            AddScore(1);
        }
    }

    public void RestartGame()
    {
        ScreenManager.Instance.StartGame();
        isGameOver = false;
    }

    public void QuitGame()
    {
        playerScore = 0;
        isGameOver = false;
        ScreenManager.Instance.QuitToMain();
    }

    public void GameOver()
    {
        playerScore = 0;
        ScreenManager.Instance.ShowGameOver();
        isGameOver = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lvl 1")
        {
            scoreObject = GameObject.FindGameObjectWithTag("ScoreText");

            if (scoreObject != null)
            {
                scoreText = scoreObject.GetComponent<Text>();
            }
            else
            {
                scoreText = null;
            }
        }
        else
        {
            scoreObject = null;
            scoreText = null;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


}
