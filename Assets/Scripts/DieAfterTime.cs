﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour {


    public float lifetime;
    private float life;

	// Use this for initialization
	void Start () {
        life = 0;
	}
	
	// Update is called once per frame
	void Update () {
        life += Time.deltaTime;
        if(life > lifetime)
        {
            Destroy(gameObject);
        }
	}
}
