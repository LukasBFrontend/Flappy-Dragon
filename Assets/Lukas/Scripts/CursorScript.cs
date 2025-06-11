using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach this script to a GameObject with a Collider, then mouse over the object to see your cursor change.
public class CursorScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        Cursor.visible = SceneManager.GetActiveScene().name == "Main menu";
    }

    void Update()
    {
    }
}
