using UnityEngine;

public class MouseOver : MonoBehaviour
{
    // Attach this script to a GameObject with a Collider, then mouse over the object to see your cursor change.
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseOver()
    {
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseExit()
    {
        // Pass 'null' to the texture parameter to use the default system cursor.
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
