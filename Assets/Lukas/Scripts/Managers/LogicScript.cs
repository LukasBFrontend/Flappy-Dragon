using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : Singleton<LogicScript>
{
    [HideInInspector] public static int playerScore = 0;
    [HideInInspector] public static Vector2 respawnPoint = new(0, 0);
    [HideInInspector] public bool isGameOver, isPaused, isGameWon, isBossFight = false;
    [HideInInspector] public bool tutorialIsActive = true;
    private GameObject scoreObject, deathCountObject, player, moving;
    private static int latestScore = 0;
    [HideInInspector] public static int deathCount = 0;
    private Text scoreText, deathCountText;
    // respawn transition variables
    private bool respawnIsQued, hasShownGameWon = false;
    private GroundMoveScript moveScript;
    [HideInInspector] public HeartsManager heartsManager;
    private float respawnTimer = .75f;

    void Start()
    {
        Debug.Log(respawnPoint);
        player = GameObject.FindGameObjectWithTag("Player");
        moving = GameObject.FindGameObjectWithTag("Moving");
        moveScript = moving?.GetComponent<GroundMoveScript>();
        heartsManager = GameObject.FindGameObjectWithTag("HeartsManager")?.GetComponent<HeartsManager>();

        if (scoreText) scoreText.text = playerScore.ToString();

        if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            InvokeRepeating("TickingScore", 0f, 0.1f);
        }


        if (ScreenManager.Instance.startAtBoss)
        {
            tutorialIsActive = false;
        }
        if (ScreenManager.Instance.startAtTutorial)
        {

            tutorialIsActive = true;
        }

        if (Tutorial.tutorialCompleted)
        {
            if (!ScreenManager.Instance.startAtBoss && respawnPoint.x > 0) SetRespawn(new(0, 0));
            ScreenManager.Instance.startAtTutorial = false;
            tutorialIsActive = false;
        }

        if (moveScript) moveScript.gameObject.transform.position = respawnPoint;
    }

    void Update()
    {


        if (respawnIsQued) RespawnTransition();
    }

    public void SetRespawn(Vector2 respawn)
    {
        respawnPoint = respawn;
        latestScore = playerScore;
        isGameWon = false;
    }

    public Vector2 GetRespawn()
    {
        return respawnPoint;
    }

    public void IncrementDeathCount()
    {
        deathCount++;
    }

    public bool LvlIsActive()
    {
        return !isGameWon && !isGameOver && !isPaused;
    }
    public void AddScore(int points)
    {
        playerScore += points;
        //heartsManager.Progress(points);
        scoreText.text = playerScore.ToString();
    }

    public void ScoreTick(int points)
    {
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }

    public void TickingScore()
    {
        if (!tutorialIsActive && !isGameWon && !isGameOver && !isPaused && !isBossFight && SceneManager.GetActiveScene().name == "Lvl 1")
        {
            ScoreTick(1);
        }
    }

    public void RestartGame()
    {
        playerScore = 0;
        latestScore = 0;
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
        if (!isGameOver)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            PlayerScript.hearts = 3;
            ScreenManager.Instance.ShowGameWon();
            isGameWon = true;
        }
    }

    public void GameOver()
    {
        if (!isGameOver && !hasShownGameWon)
        {
            playerScore = 0;
            IncrementDeathCount();
            SetRespawn(new(0, 0));
            if (ScreenManager.Instance.startAtBoss) SetRespawn(new(-775, 0));
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            deathCountText.text = "Variant: X" + deathCount.ToString();
            ScreenManager.Instance.ShowGameOver();
            isGameOver = true;
            hasShownGameWon = true;
        }
    }

    private void RespawnTransition()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }
        else
        {
            playerScore = latestScore;
            ScreenManager.Instance.StartGame();
        }
    }
    public void Respawn()
    {
        respawnIsQued = true;
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
            deathCountObject = GameObject.FindGameObjectWithTag("DeathCountText");
            scoreObject = GameObject.FindGameObjectWithTag("ScoreText");

            if (scoreObject != null)
            {
                scoreText = scoreObject.GetComponent<Text>();
            }
            else
            {
                scoreText = null;
            }

            if (deathCountObject != null)
            {
                deathCountText = deathCountObject.GetComponent<Text>();
                deathCountText.text = "Variant: X" + deathCount.ToString();
            }
            else
            {
                deathCountText = null;
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
