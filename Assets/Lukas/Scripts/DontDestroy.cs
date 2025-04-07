using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private static bool alreadyExists = false;

    private void Awake()
    {
        if (alreadyExists)
        {
            Destroy(gameObject); // Destroy this duplicate
            return;
        }

        alreadyExists = true;
        DontDestroyOnLoad(gameObject); // Persist this GameObject
    }
}
