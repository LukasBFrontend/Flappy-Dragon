using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject lvlMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ScreenManager.Instance.OpenSoundMenu();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScreenManager.Instance.OpenLvlMenu();
        }
    }
}
