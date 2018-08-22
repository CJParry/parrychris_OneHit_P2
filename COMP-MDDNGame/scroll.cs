using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour {


public float speed = 0.5f;
public float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 offset = new Vector2 (startTime * speed, 0);
        startTime+= 0.001f;
		GetComponent<Renderer>().material.mainTextureOffset = offset;﻿
		if(startTime > 1.5){
			startTime = 0;
		}

	}
}
