using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;
    private string lastSceneName;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        lastSceneName = SceneManager.GetActiveScene().name;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != lastSceneName)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
        else
        {
            lastSceneName = scene.name;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }
}
