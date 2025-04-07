using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    public void OnResumeClicked()
    {
        ScreenManager.Instance.CloseLvlMenu();
        LogicScript.Instance.UnPauseGame();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }
    public void OnQuitClicked()
    {
        ScreenManager.Instance.QuitToMain();
        LogicScript.Instance.UnPauseGame();
    }
}
