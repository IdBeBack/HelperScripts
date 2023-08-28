using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private CustomCursor defaultCursor;

    private CustomCursor currCursor;

    [HideInInspector] public bool displayCursor = true;

    private void OnApplicationFocus(bool focus) => Cursor.visible = !focus;

    private void OnGUI()
    {
        if (currCursor && displayCursor)
        {
            Rect rect = new Rect(Event.current.mousePosition.x - currCursor.hotspot.x,
                Event.current.mousePosition.y + currCursor.hotspot.y, currCursor.width, currCursor.height);

            GUI.DrawTexture(rect, currCursor.cursor);
        }
    }

    public void SetCursor(bool value, CustomCursor cursor = null) => currCursor = (value && cursor) ? cursor : defaultCursor;
}