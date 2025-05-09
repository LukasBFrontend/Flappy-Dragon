using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : Singleton<LogicScript>
{
    [HideInInspector] public int playerScore = 0;
    [HideInInspector] public bool isGameOver, isPaused, isGameWon, isBossFight = false;
    private GameObject scoreObject, player;
    private Text scoreText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (scoreText) scoreText.text = playerScore.ToString();

        if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            InvokeRepeating("TickingScore", 0f, 0.1f);
        }
    }

    public bool LvlIsActive()
    {
        return !isGameWon && !isGameOver && !isPaused && !isBossFight;
    }
    public void AddScore(int points)
    {
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }

    public void TickingScore()
    {
        if (!isGameWon && !isGameOver && !isPaused && !isBossFight && SceneManager.GetActiveScene().name == "Lvl 1")
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

    public void GameWon()
    {
        if (!isGameOver && !isGameWon)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            ScreenManager.Instance.ShowGameWon();
            isGameWon = true;
        }
    }

    public void GameOver()
    {
        if (!isGameOver && !isGameWon)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            playerScore = 0;
            ScreenManager.Instance.ShowGameOver();
            isGameOver = true;
        }
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
