using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : Singleton<LogicScript>
{
    [SerializeField] private AudioClip musicClip;
    public int playerScore = 0;
    private GameObject scoreObject;
    private Text scoreText;

    [HideInInspector]
    public bool isGameOver = false;

    [ContextMenu("Increase Score")]

    void Start()
    {
        //SoundFXManager.Instance.playSoundFXClip(musicClip, transform, 0.6f);
        if (scoreText) scoreText.text = playerScore.ToString();

        Debug.Log("Start");
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
        if (!isGameOver && SceneManager.GetActiveScene().name == "Lvl 1")
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
                Debug.LogWarning("Player not found in Lvl 1.");
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
