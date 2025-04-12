using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private GameObject player;
    private PlayerScript playerScript;

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lvl 1")
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                playerScript = player.GetComponent<PlayerScript>();
            }
            else
            {
                playerScript = null;
            }
        }
        else
        {
            player = null;
            playerScript = null;
        }
    }

    void Update()
    {
        if (playerScript == null) return;

        if (Input.GetKeyDown(KeyCode.O) && playerScript.playerIsAlive)
        {
            ScreenManager.Instance.OpenSoundMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && playerScript.playerIsAlive && !LogicScript.Instance.isPaused)
        {
            ScreenManager.Instance.OpenLvlMenu();
            LogicScript.Instance.PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LogicScript.Instance.PauseGame();
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
