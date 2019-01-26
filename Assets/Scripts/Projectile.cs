using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float maxLifeTime;
    private float currentLife;
    public float speed;
    private int playerThatFiredMe;
    public float rotationOffset;

	// Use this for initialization
	void Start () {
        currentLife = 0;
        transform.localEulerAngles = new Vector3(Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));
	}
	
	// Update is called once per frame
	void Update () {
        currentLife += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (currentLife >= maxLifeTime)
        {
            Destroy(gameObject);
        }
	}

    public void SetPlayerNumber(int num)
    {
        playerThatFiredMe = num;
        print("My number is " + playerThatFiredMe);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //TODO deal damage
        }
    }
}
