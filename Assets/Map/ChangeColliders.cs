using UnityEngine;

/// <summary>
/// Skript zum anpassen und verschieben der vorhandenen Collider in Kindobjekten.
/// Vorsicht bei der benutzung damit die Szene nicht zerstört wird.
/// </summary>

[ExecuteInEditMode]
public class ChangeColliders : MonoBehaviour
{

    public bool hasRunOnce = false;

    void Start()
    {
        if (hasRunOnce == false)
        {
            Component[] components = GetComponentsInChildren(typeof(CapsuleCollider2D), true);
            foreach (CapsuleCollider2D c in components)
            {
                Vector3 oldPosition = c.transform.position;
                c.size = new Vector2(3f, 1.5f);
                c.transform.position = (oldPosition - new Vector3(0, 0.8f, 0));
            }
            hasRunOnce = true;
        }
    }
}
