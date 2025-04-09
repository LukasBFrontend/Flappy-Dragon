using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        ScreenManager.Instance.StartGame();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }
}
