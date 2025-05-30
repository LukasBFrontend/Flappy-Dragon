using UnityEditor;
using UnityEngine;

public class StartMenu : MenuCanvasBase
{
    public void OnStartClicked()
    {
        ScreenManager.Instance.OpenStartSelectMenu();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
