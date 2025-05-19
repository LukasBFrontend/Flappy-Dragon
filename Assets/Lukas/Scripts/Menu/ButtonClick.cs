using UnityEngine;

public class ButtonClick : MenuCanvasBase
{
    [SerializeField] private AudioClip clickClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 30;
    public void OnButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(clickClip, transform, audioVolume);
    }
}
