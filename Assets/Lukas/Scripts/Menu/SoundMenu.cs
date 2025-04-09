using UnityEngine;

public class SoundMenu : MonoBehaviour
{
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
        ScreenManager.Instance.CloseSoundMenu();
    }
}
