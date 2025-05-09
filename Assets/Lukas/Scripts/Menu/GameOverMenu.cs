using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AudioClip restartClickClip;
    [SerializeField] private AudioClip quitClickClip;
    [SerializeField][Range(0, 100)] private int audioVolume = 50;
    public void OnRestartButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(restartClickClip, transform, audioVolume);

        ScreenManager.Instance.StartGame();
        LogicScript.Instance.isGameOver = false;
    }
    public void OnQuitButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(quitClickClip, transform, audioVolume);
        ScreenManager.Instance.QuitToMain();
        LogicScript.Instance.playerScore = 0;
        LogicScript.Instance.isGameOver = false;
    }
}
