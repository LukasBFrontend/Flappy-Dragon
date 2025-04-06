using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    public int playerScore = 0;
    public Text scoreText;
    public GameObject gameOverScreen;

    [HideInInspector]
    public bool isGameOver = false;

    [ContextMenu("Increase Score")]
    public void AddScore(int points)
    {
        playerScore += points;
        scoreText.text = playerScore.ToString();
    }

    public void TickingScore()
    {
        if (!isGameOver)
        {
            AddScore(1);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameOver = true;
    }

    void Start()
    {
        SoundFXManager.instance.playSoundFXClip(musicClip, transform, 0.6f);
        scoreText.text = playerScore.ToString();
        InvokeRepeating("TickingScore", 0f, 0.1f);
    }

    void Update()
    {

    }
}
