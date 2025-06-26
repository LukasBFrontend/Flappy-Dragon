using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSelect : MenuCanvasBase
{
    private bool bossClicked = false;

    public void OnBeginningClicked()
    {
        ScreenManager.Instance.startAtBoss = false;
        ScreenManager.Instance.startAtTutorial = true;
        ScreenManager.Instance.StartGame();
    }

    public void OnBossClicked()
    {
        ScreenManager.Instance.startAtBoss = true;
        ScreenManager.Instance.startAtTutorial = false;
        ScreenManager.Instance.StartGame();
    }

    public void OnBackClicked()
    {
        ScreenManager.Instance.CloseStartSelectMenu();
    }
}
