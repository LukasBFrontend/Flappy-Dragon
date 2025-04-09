using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private AudioClip backClickClip;
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
        SoundFXManager.Instance.playSoundFXClip(backClickClip, transform, .3f);
        ScreenManager.Instance.CloseSoundMenu();
    }
}
