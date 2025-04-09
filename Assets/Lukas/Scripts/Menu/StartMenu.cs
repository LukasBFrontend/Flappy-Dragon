using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioClip startClickClip;
    [SerializeField] private AudioClip optionsClickClip;
    public void OnStartClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(startClickClip, transform, .3f);
        ScreenManager.Instance.StartGame();
    }
    public void OnOptionsClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(optionsClickClip, transform, .3f);
        ScreenManager.Instance.OpenSoundMenu();
    }
}
