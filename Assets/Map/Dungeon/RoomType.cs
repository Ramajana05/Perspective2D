using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{

    public int type;

    /// <summary>
    /// Zerstört den Raum
    /// </summary>
    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
