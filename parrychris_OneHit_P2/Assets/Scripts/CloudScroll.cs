using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes cloud loop accross background
public class CloudScroll : MonoBehaviour
{ 
    public float speed = 0.5f;
    public float duration;

    // Use this for initialization
    void Start()
    {
        duration = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(duration * speed, 0);
        duration += 0.001f;
        GetComponent<Renderer>().material.mainTextureOffset = offset;
        if (duration > 1.5)
        {
            duration = 0;
        }
    }
}