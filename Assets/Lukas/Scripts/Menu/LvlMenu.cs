using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    public void OnResumeClicked()
    {
        ScreenManager.Instance.CloseLvlMenu();
    }
    public void OnOptionsClicked()
    {
        ScreenManager.Instance.OpenSoundMenu();
    }
    public void OnQuitClicked()
    {
        ScreenManager.Instance.QuitToMain();
    }
}
