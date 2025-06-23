using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PersistentAudio : MonoBehaviour
{
    [Tooltip("Unique identifier for this persistent audio object (e.g., 'MainMusic', 'RainAmbience')")]
    [SerializeField] private string uniqueID;

    private static Dictionary<string, PersistentAudio> instances = new();

    private string lastSceneName;

    void Awake()
    {
        if (string.IsNullOrEmpty(uniqueID))
        {
            Debug.LogWarning($"PersistentAudio on '{gameObject.name}' has no uniqueID. Destroying.");
            Destroy(gameObject);
            return;
        }

        if (instances.TryGetValue(uniqueID, out var existingInstance) && existingInstance != this)
        {
            Destroy(gameObject); // Duplicate with same ID, remove
            return;
        }

        instances[uniqueID] = this;
        DontDestroyOnLoad(gameObject);

        lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != lastSceneName)
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);

            lastSceneName = scene.name;
        }
    }

    private void OnDestroy()
    {
        if (instances.TryGetValue(uniqueID, out var current) && current == this)
        {
            instances.Remove(uniqueID);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
