using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
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
                Debug.LogWarning("Player not found in Lvl 1.");
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
        if (Input.GetKeyDown(KeyCode.Escape) && playerScript.playerIsAlive)
        {
            ScreenManager.Instance.OpenLvlMenu();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        if (ScreenManager.Instance != null)
        {
            ScreenManager.Instance.OnQuitToMain += HandleQuitToMain;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        if (ScreenManager.Instance != null)
        {
            ScreenManager.Instance.OnQuitToMain -= HandleQuitToMain;
        }
    }

    private void HandleQuitToMain()
    {
        Debug.Log("InputManager detected QuitToMain.");
    }
}
