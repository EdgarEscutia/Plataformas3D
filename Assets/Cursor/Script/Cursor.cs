using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
       //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
