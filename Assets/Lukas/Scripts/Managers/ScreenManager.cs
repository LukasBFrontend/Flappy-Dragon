using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ScreenManager : Singleton<ScreenManager>
{
    private GameObject startMenu, soundMenu, lvlMenu, gameOverMenu, gameWonMenu, bossCanvas, playerCanvas, scoreCanvas, startSelectMenu;
    private GameObject lvl;
    public bool startAtBoss = false;
    public bool startAtTutorial = true;
    public void Start()
    {
        CacheMenus();
    }
    public void StartGame()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Lvl 1");
    }

    public void StartAtBoss()
    {
        startAtBoss = true;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Lvl 1");
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CacheMenus();

        lvl = GameObject.FindGameObjectWithTag("Moving");

        if (lvl != null)
        {
            if (startAtBoss) lvl.transform.position = new Vector2(-775, 0);
            else if (startAtTutorial)
            {
                lvl.transform.position = new Vector2(28, 0);
            }
        }

        if (scene.name == "Lvl 1")
        {
            HideMenu(gameOverMenu);

            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
        if (scene.name == "Main menu")
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }

    private void CacheMenus()
    {
        startMenu = GameObject.FindGameObjectWithTag("StartMenu");
        soundMenu = GameObject.FindGameObjectWithTag("SoundMenu");
        lvlMenu = GameObject.FindGameObjectWithTag("LvlMenu");
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOverMenu");
        gameWonMenu = GameObject.FindGameObjectWithTag("GameWonMenu");
        bossCanvas = GameObject.FindGameObjectWithTag("BossCanvas");
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas");
        startSelectMenu = GameObject.FindGameObjectWithTag("StartSelectMenu");
    }

    public void QuitToMain()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Main menu");
    }

    public void OpenLvlMenu()
    {
        if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            EventSystem.current.SetSelectedGameObject(null);

            ShowMenu(lvlMenu);
        }
    }

    public void CloseLvlMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        HideMenu(lvlMenu);
    }

    public void OpenSoundMenu()
    {
        if (startMenu)
        {
            HideMenu(startMenu);
        }
        if (lvlMenu)
        {
            HideMenu(lvlMenu);
        }
        ShowMenu(soundMenu);
    }

    public void CloseSoundMenu()
    {
        HideMenu(soundMenu);

        if (SceneManager.GetActiveScene().name == "Main menu")
        {
            ShowMenu(startMenu);
        }
        else if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            ShowMenu(lvlMenu);
        }
    }

    public void ShowGameOver()
    {
        ShowMenu(gameOverMenu);
    }

    public void ShowGameWon()
    {
        ShowMenu(gameWonMenu);
    }

    public void ShowBossCanvas()
    {
        ShowMenu(bossCanvas);
    }

    public void HideBossCanvas()
    {
        HideMenu(bossCanvas);
    }

    public void ShowPlayerCanvas()
    {
        ShowMenu(playerCanvas);
    }

    public void HidePlayerCanvas()
    {
        HideMenu(playerCanvas);
    }

    public void ShowScoreCanvas()
    {
        ShowMenu(scoreCanvas);
    }
    public void HideScoreCanvas()
    {
        HideMenu(scoreCanvas);
    }

    public void OpenStartSelectMenu()
    {
        HideMenu(startMenu);
        ShowMenu(startSelectMenu);
    }
    public void CloseStartSelectMenu()
    {
        HideMenu(startSelectMenu);
        ShowMenu(startMenu);
    }
    private void ShowMenu(GameObject menu)
    {
        var canvasGroup = menu.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            Debug.Log("No canvas group found on " + menu.name);
        }
    }

    private void HideMenu(GameObject menu)
    {
        var canvasGroup = menu.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
