using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections.Generic;

/// <summary>
/// Every child that is added to gameObject is converted to UI
/// </summary>
[ExecuteAlways]
public class UIChildrenConverter : MonoBehaviour
{
    private List<GameObject> children = new();

    public event Action<GameObject> onChildConverted;

    private void OnEnable()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private void OnDisable()
    {
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
    }

    private void OnHierarchyChanged()
    {
        List<GameObject> newChildren = transform.GetChildren().ToList();

        foreach (GameObject child in newChildren)
        {
            if (!children.Contains(child))
            {
                Convert(child);
                children.Add(child);
                onChildConverted?.Invoke(child);
            }
        }
        children = newChildren;
    }

    [Button("Convert all children")]
    private void ConvertAllChildren()
    {
        foreach (GameObject child in children)
            Convert(child);
    }

    /// <summary>
    /// Converts GameObject to UI GameObject
    /// </summary>
    private void Convert(GameObject source, bool saveScale = true)
    {
        if (!source || source.GetComponent<RectTransform>())
            return;

        if (PrefabUtility.IsPartOfAnyPrefab(source))
            PrefabUtility.UnpackPrefabInstance(source, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);

        source.name += " UI";

        source.AddComponent<RectTransform>();
        Image image = source.AddComponent<Image>();

        SpriteRenderer renderer = source.GetComponent<SpriteRenderer>();
        image.sprite = renderer.sprite;
        image.color = renderer.color;

        foreach (Component component in source.GetComponents<Component>())
        {
            if (!(component is RectTransform) && !(component is Image) && source.CanDestroy(component))
                DestroyImmediate(component);
        }

        if (!saveScale)
            source.transform.localScale = Vector3.one;
    }
}