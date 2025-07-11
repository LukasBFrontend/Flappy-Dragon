using UnityEditor;
using UnityEngine;

public class StartMenu : MenuCanvasBase
{
    public void OnStartClicked()
    {
        //ScreenManager.Instance.OpenStartSelectMenu();
        ScreenManager.Instance.startAtBoss = false;
        ScreenManager.Instance.startAtTutorial = true;
        ScreenManager.Instance.StartGame();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnCreditsClicked()
    {
        ScreenManager.Instance.CloseStartMenu();
        ScreenManager.Instance.OpenCreditsCanvas();
    }
}
