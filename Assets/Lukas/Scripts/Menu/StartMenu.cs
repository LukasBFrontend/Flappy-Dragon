using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnStartClicked()
    {
        ScreenManager.Instance.OpenStartSelectMenu();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }
}
