using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private AudioClip backClickClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    public void OnMasterVolumeChanged(float level)
    {
        SoundMixerManager.Instance.SetMasterVolume(level);
    }
    public void OnSoundFXVolumeChanged(float level)
    {
        SoundMixerManager.Instance.SetSoundFXVolume(level);
    }
    public void OnMusicVolumeChanged(float level)
    {
        SoundMixerManager.Instance.SetMusicVolume(level);
    }
    public void OnBackButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(backClickClip, transform, audioVolume);
        ScreenManager.Instance.CloseSoundMenu();
    }
}
