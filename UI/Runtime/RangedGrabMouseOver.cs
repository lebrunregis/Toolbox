using UnityEngine;

public class RangedGrabMouseOver : MonoBehaviour
{
    // Attach this script to a GameObject with a Collider, then mouse over the object to see your cursor change.
    public Texture2D cursorTextureDefault; 
    public Vector2 hotSpotDefault = Vector2.zero;
    public Texture2D cursorTextureEnabled;
        public Vector2 hotSpotEnabled = Vector2.zero;
    
    public CursorMode cursorMode = CursorMode.Auto;



    private void OnMouseEnter()
    {
      //  Cursor.SetCursor(cursorTextureDefault, hotSpot, cursorMode);
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
      //  Cursor.SetCursor(cursorTextureDefault, hotSpot, cursorMode);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
