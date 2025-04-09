using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    [SerializeField] private AudioClip resumeClickClip;
    [SerializeField] private AudioClip optionsClickClip;
    [SerializeField] private AudioClip quitClickClip;
    public void OnResumeClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(resumeClickClip, transform, .3f);
        ScreenManager.Instance.CloseLvlMenu();
        LogicScript.Instance.UnPauseGame();
    }
    public void OnOptionsClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(optionsClickClip, transform, .3f);
        ScreenManager.Instance.OpenSoundMenu();
    }
    public void OnQuitClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(quitClickClip, transform, .3f);
        ScreenManager.Instance.QuitToMain();
        LogicScript.Instance.UnPauseGame();
    }
}
