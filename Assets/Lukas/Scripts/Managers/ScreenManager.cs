using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : Singleton<ScreenManager>
{
    private GameObject startMenu;
    private GameObject soundMenu;
    private GameObject lvlMenu;
    private GameObject gameOverMenu;

    public void Start()
    {
        CacheMenus();
    }
    public void StartGame()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Lvl 1");
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        CacheMenus();

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
    }

    public void QuitToMain()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Main menu");
    }

    public void OpenLvlMenu()
    {
        if (SceneManager.GetActiveScene().name == "Lvl 1") ShowMenu(lvlMenu);
    }

    public void CloseLvlMenu()
    {
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

    private void ShowMenu(GameObject menu)
    {
        var canvasGroup = menu.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
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
