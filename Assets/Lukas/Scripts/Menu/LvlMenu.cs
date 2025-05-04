using UnityEngine;

public class LvlMenu : MenuCanvasBase
{
    [SerializeField] private AudioClip resumeClickClip;
    [SerializeField] private AudioClip optionsClickClip;
    [SerializeField] private AudioClip quitClickClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    public void OnResumeClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(resumeClickClip, transform, audioVolume);
        ScreenManager.Instance.CloseLvlMenu();
        LogicScript.Instance.UnPauseGame();
    }
    public void OnOptionsClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(optionsClickClip, transform, audioVolume);
        ScreenManager.Instance.OpenSoundMenu();
    }
    public void OnQuitClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(quitClickClip, transform, audioVolume);
        ScreenManager.Instance.QuitToMain();
        LogicScript.Instance.UnPauseGame();
    }
}
