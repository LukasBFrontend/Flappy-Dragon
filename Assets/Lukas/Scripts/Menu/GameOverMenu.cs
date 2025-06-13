using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void OnRestartButtonClicked()
    {
        ScreenManager.Instance.StartGame();
        LogicScript.Instance.isGameOver = false;
    }
    public void OnQuitButtonClicked()
    {
        ScreenManager.Instance.QuitToMain();
        LogicScript.playerScore = 0;
        LogicScript.Instance.isGameOver = false;
    }
}
