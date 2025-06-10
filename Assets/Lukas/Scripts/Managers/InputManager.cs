using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private GameObject player, lvlMenu, startMenu;
    private PlayerScript playerScript;
    private StartMenu startMenuScript;
    private LvlMenu lvlMenuScript;

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lvl 1")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            lvlMenu = GameObject.FindGameObjectWithTag("LvlMenu");


            if (player != null)
            {
                playerScript = player.GetComponent<PlayerScript>();
            }
            else
            {
                playerScript = null;
            }

            if (lvlMenu != null)
            {
                lvlMenuScript = lvlMenu.GetComponent<LvlMenu>();
            }
            else
            {
                lvlMenuScript = null;
            }

            startMenu = null;
            startMenuScript = null;
        }
        else
        {
            startMenu = GameObject.FindGameObjectWithTag("StartMenu");

            if (startMenu != null)
            {
                startMenuScript = startMenu.GetComponent<StartMenu>();
            }
            else
            {
                startMenuScript = null;
            }

            player = null;
            playerScript = null;
            lvlMenu = null;
            lvlMenuScript = null;

        }
    }

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (verticalInput != 0 && EventSystem.current.currentSelectedGameObject == null)
        {
            startMenuScript?.SelectFirstButton();
            lvlMenuScript?.SelectFirstButton();
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

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
        /*         if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                } */
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
