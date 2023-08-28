using UnityEngine;

[CreateAssetMenu(menuName = "Custom Cursor")]
public class CustomCursor : ScriptableObject
{
    public Texture2D cursor;
    public Vector2 hotspot;

    [Space(8f)]
    public bool scaleManually;
    public float width;
    public float height;

    private float prevWidth;
    private float prevHeight;

    public void OnValidate()
    {
        if (!scaleManually && cursor)
        {
            if (width != prevWidth)
                height = cursor.height * width / cursor.width;
            else if (height != prevHeight)
                width = cursor.width * height / cursor.height;

            prevWidth = width;
            prevHeight = height;
        }
    }
}