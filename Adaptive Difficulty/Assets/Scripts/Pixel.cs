using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    public SpriteRenderer sr;
    public float val;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = new Color(255 - (val * 255), 0, 0);
    }
}
