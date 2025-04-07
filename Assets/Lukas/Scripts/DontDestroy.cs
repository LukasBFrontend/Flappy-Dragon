using UnityEngine;
using System.Collections.Generic;

public class DontDestroy : MonoBehaviour
{
    private static HashSet<string> existingObjects = new HashSet<string>();

    private void Awake()
    {
        string key = gameObject.name;

        if (existingObjects.Contains(key))
        {
            Debug.Log($"{key} was destroyed — duplicate");
            Destroy(gameObject);
            return;
        }

        existingObjects.Add(key);
        DontDestroyOnLoad(gameObject);
    }
}
