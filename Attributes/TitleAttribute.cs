using System;
using UnityEngine;
using UnityEditor;

public enum HeaderAlignment
{
    Left,
    Right,
    Center,
    LeftCenter,
    RightCenter,
}

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
public class TitleAttribute : PropertyAttribute
{
    public string text = string.Empty;

    public float topOffset = 10f;
    public float bottomOffset = 3f;

    public float leftOffset = 0f;

    public int fontSize = 12;

    public HeaderAlignment alignment = HeaderAlignment.Left;

    public FontStyle fontStyle = FontStyle.Bold;

    public TitleAttribute() { }

    public TitleAttribute(string text)
    {
        this.text = text;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset) : this(text)
    {
        this.topOffset = topOffset;
        this.bottomOffset = bottomOffset;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, float leftOffset) 
        : this(text, topOffset, bottomOffset)
    {
        this.leftOffset = leftOffset;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, float leftOffset, int fontSize) 
        : this(text, topOffset, bottomOffset, leftOffset)
    {
        this.fontSize = fontSize;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, HeaderAlignment alignment) : this(text, topOffset, bottomOffset)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, FontStyle fontStyle) : this(text, topOffset, bottomOffset)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, int fontSize)
        : this(text, topOffset, bottomOffset)
    {
        this.fontSize = fontSize;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, int fontSize, 
        HeaderAlignment alignment) : this(text, topOffset, bottomOffset, fontSize)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, int fontSize, 
        FontStyle fontStyle) : this(text, topOffset, bottomOffset, fontSize)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, int fontSize, 
        HeaderAlignment alignment, FontStyle fontStyle) : this(text, topOffset, bottomOffset, fontSize, alignment)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, float leftOffset, int fontSize, 
        HeaderAlignment alignment) : this(text, topOffset, bottomOffset, leftOffset, fontSize)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, float leftOffset, int fontSize, 
        FontStyle fontStyle) : this(text, topOffset, bottomOffset, leftOffset, fontSize)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float topOffset, float bottomOffset, float leftOffset, int fontSize, 
        HeaderAlignment alignment, FontStyle fontStyle) : this(text, topOffset, bottomOffset, leftOffset, fontSize, alignment)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float leftOffset) : this(text)
    {
        this.leftOffset = leftOffset;
    }

    public TitleAttribute(string text, float leftOffset, HeaderAlignment alignment) : this(text, leftOffset)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, float leftOffset, FontStyle fontStyle) : this(text, leftOffset)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, float leftOffset, HeaderAlignment alignment, FontStyle fontStyle) 
        : this(text, leftOffset, alignment)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, int fontSize) : this(text)
    {
        this.fontSize = fontSize;
    }

    public TitleAttribute(string text, int fontSize, HeaderAlignment alignment) : this(text, fontSize)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, int fontSize, FontStyle fontStyle) : this(text, fontSize)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, int fontSize, HeaderAlignment alignment, FontStyle fontStyle) 
        : this(text, fontSize, alignment)
    {
        this.fontStyle = fontStyle;
    }

    public TitleAttribute(string text, HeaderAlignment alignment) : this(text)
    {
        this.alignment = alignment;
    }

    public TitleAttribute(string text, FontStyle fontStyle) : this(text)
    {
        this.fontStyle = fontStyle;
    }
}

[CustomPropertyDrawer(typeof(TitleAttribute))]
public class TitleDrawer : DecoratorDrawer
{
    private readonly GUIStyle style = new(EditorStyles.label);

    private TitleAttribute TitleAttribute => (TitleAttribute)attribute;

    private float SpacesHeight => TitleAttribute.topOffset + TitleAttribute.bottomOffset;

    private float FieldWidth
    {
        get
        {
            GUIContent content = new GUIContent(TitleAttribute.text);

            Vector2 size = style.CalcSize(content);

            return size.x;
        }
    }

    public override float GetHeight() => GetSingleLineHeight() + SpacesHeight;

    public override void OnGUI(Rect rect)
    {
        TitleAttribute att = TitleAttribute;

        {
            rect.y -= SpacesHeight / 2f - att.topOffset;

            float freeWidth = rect.width - FieldWidth, offset = 6f;

            float newWidth = rect.xMin + att.alignment switch
            {
                HeaderAlignment.Left => 0f,
                HeaderAlignment.Right => freeWidth,
                HeaderAlignment.Center => freeWidth * .5f - offset,
                HeaderAlignment.LeftCenter => freeWidth * .25f - offset,
                HeaderAlignment.RightCenter => freeWidth * .75f - offset,
                _ => rect.x
            };

            if (newWidth >= 0f)
                rect.x = newWidth + att.leftOffset;
        }

        {
            style.fontStyle = att.fontStyle;

            if (att.fontSize > 0)
                style.fontSize = att.fontSize;
        }

        EditorGUI.LabelField(rect, att.text, style);
    }

    private float GetSingleLineHeight() => style.lineHeight;
}