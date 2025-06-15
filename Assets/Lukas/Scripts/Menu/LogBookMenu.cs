using UnityEngine;

public class LogBookMenu : MonoBehaviour
{
    [SerializeField] private GameObject onBackClicked;
    public void OnBackClicked()
    {
        ScreenManager.Instance.HideMenu(gameObject);
        ScreenManager.Instance.ShowMenu(onBackClicked);
    }
}
