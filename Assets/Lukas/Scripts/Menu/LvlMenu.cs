using UnityEngine;

public class LvlMenu : MenuCanvasBase
{
    [SerializeField] private GameObject onLogBookClicked;
    public void OnLogbookClicked()
    {
        ScreenManager.Instance.HideMenu(gameObject);
        ScreenManager.Instance.ShowMenu(onLogBookClicked);
    }
    public void OnResumeClicked()
    {
        Cursor.visible = false;
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
