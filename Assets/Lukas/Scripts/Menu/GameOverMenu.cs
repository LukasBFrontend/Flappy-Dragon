using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AudioClip restartClickClip;
    [SerializeField] private AudioClip quitClickClip;
    public void OnRestartButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(restartClickClip, transform, .3f);
        ScreenManager.Instance.StartGame();
        LogicScript.Instance.isGameOver = false;
    }
    public void OnQuitButtonClicked()
    {
        SoundFXManager.Instance.playSoundFXClip(quitClickClip, transform, .3f);
        ScreenManager.Instance.QuitToMain();
        LogicScript.Instance.playerScore = 0;
        LogicScript.Instance.isGameOver = false;
    }
}
