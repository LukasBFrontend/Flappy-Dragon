using UnityEngine;
using System.Collections.Generic;

public class DontDestroyGroup : MonoBehaviour
{
    [Tooltip("List of GameObjects that should not be destroyed on scene load.")]
    [SerializeField] private GameObject[] persistentObjects;
    private static HashSet<string> preservedNames = new HashSet<string>();

    private void Awake()
    {
        foreach (var obj in persistentObjects)
        {
            if (obj == null) continue;

            string objName = obj.name;

            if (preservedNames.Contains(objName))
            {
                Destroy(obj);
            }
            else
            {
                preservedNames.Add(objName);
                DontDestroyOnLoad(obj);
            }
        }

        string managerName = gameObject.name;
        if (!preservedNames.Contains(managerName))
        {
            preservedNames.Add(managerName);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
