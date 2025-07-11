using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        ScreenManager.Instance.CloseCreditsCanvas();
        ScreenManager.Instance.OpenStartMenu();
    }
}
