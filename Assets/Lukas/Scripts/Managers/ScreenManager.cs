using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : Singleton<ScreenManager>
{
    public GameObject startMenu;
    public GameObject soundMenu;
    public GameObject lvlMenu;

    public void StartGame()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        SceneManager.LoadScene("Lvl 1");
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lvl 1")
        {
            startMenu.SetActive(false);

            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
        if (scene.name == "Main menu")
        {
            lvlMenu.SetActive(false);
            startMenu.SetActive(true);

            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }
    public void QuitToMain()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        SceneManager.LoadScene("Main menu");
    }

    public void OpenLvlMenu()
    {
        if (SceneManager.GetActiveScene().name == "Lvl 1") lvlMenu.SetActive(true);
    }

    public void CloseLvlMenu()
    {
        lvlMenu.SetActive(false);
    }

    public void OpenSoundMenu()
    {
        if (startMenu)
        {
            startMenu.SetActive(false);
        }
        if (lvlMenu)
        {
            lvlMenu.SetActive(false);
        }
        soundMenu.SetActive(true);
    }

    public void CloseSoundMenu()
    {
        soundMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Main menu")
        {
            startMenu.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            lvlMenu.SetActive(true);
        }
    }
}
