using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCanvasBase : MonoBehaviour
{
  /// <summary>
  /// Selects the first Button component found in children of this canvas.
  /// </summary>
  public void SelectFirstButton()
  {
    // Clear current selection
    EventSystem.current.SetSelectedGameObject(null);

    // Search for the first active and interactable Button
    Button firstButton = GetComponentInChildren<Button>(true);
    if (firstButton != null && firstButton.gameObject.activeInHierarchy && firstButton.interactable)
    {
      EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
    else
    {
      Debug.LogWarning($"No valid Button found under {gameObject.name}");
    }
  }
}
