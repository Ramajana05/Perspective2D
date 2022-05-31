using UnityEngine;

/// <summary>
/// Visuelle Hilfe beim platzieren der BaumCollider
/// </summary>
[ExecuteInEditMode]
public class TreeColliderDebug : MonoBehaviour
{
    [Header("TreeCollider Debug Settings")]
    public bool ShowCollider;
    public bool DisableCollider;

    void Update()
    {
        ColliderColorChange();
        ColliderDisable();
    }
    /// <summary>
    /// Wechsele Farbe des Collider der Kindobjekte zwischen Rot und Transparent
    /// </summary>
    public void ColliderColorChange()
    {
        Component[] components = GetComponentsInChildren(typeof(SpriteRenderer), true);

        foreach (SpriteRenderer c in components)
        {
            if (ShowCollider)
            {
                c.color = Color.red;
            }
            else
            {
                c.color = Color.clear;
            }
        }
    }
    /// <summary>
    /// Deaktiviere die Collider der Kindobjekte
    /// </summary>
    public void ColliderDisable()
    {
        Component[] components = GetComponentsInChildren(typeof(CapsuleCollider2D), true);

        foreach (CapsuleCollider2D c in components)
        {
            c.enabled = !DisableCollider;
        }
    }
}
