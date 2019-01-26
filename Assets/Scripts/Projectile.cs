using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float maxLifeTime;
    private float currentLife;
    public float speed;


	// Use this for initialization
	void Start () {
        currentLife = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currentLife += Time.deltaTime;
        transform.position += new Vector3(transform.position.x, transform.position.y, speed*Time.deltaTime);
        if (currentLife >= maxLifeTime)
        {
            Destroy(gameObject);
        }
	}
}
