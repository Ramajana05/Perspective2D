using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHotFix : MonoBehaviour
{

    public float effectVolume = 0.2f;
    public float musicVolume = 0.2f;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
