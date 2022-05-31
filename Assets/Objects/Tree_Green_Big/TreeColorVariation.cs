using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColorVariation : MonoBehaviour
{
    public GameObject tree;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer treeRenderer = gameObject.GetComponent<SpriteRenderer>();    
        treeRenderer.color = new Color(Random.Range(.4f,.9f), Random.Range(0.5f, .8f), 0f, 1f);

        anim = gameObject.GetComponent<Animator>();
        anim.speed = Random.Range(1.0f, 2.5f);

    }
}
