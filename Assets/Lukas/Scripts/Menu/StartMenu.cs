using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private AudioClip startClickClip;
    [SerializeField] private AudioClip optionsClickClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    public void OnStartClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(startClickClip, transform, audioVolume);
        ScreenManager.Instance.StartGame();
    }
    public void OnOptionsClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(optionsClickClip, transform, audioVolume);
        ScreenManager.Instance.OpenSoundMenu();
    }
}
