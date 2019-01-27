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

        transform.Rotate(new Vector3(Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset)));
        Vector3 i_hate_rotations = transform.rotation.eulerAngles;

        i_hate_rotations.y /= 2.0f;
        transform.rotation = Quaternion.Euler(i_hate_rotations);
	}
	
	// Update is called once per frame
	void Update () {
        currentLife += Time.deltaTime;
        transform.Translate(transform.forward * Time.deltaTime * speed);
        //transform.LookAt(transform.forward);
        if (currentLife >= maxLifeTime)
        {
            Destroy(gameObject);
        }
	}

    public void SetPlayerNumber(int num)
    {
        playerThatFiredMe = num;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //TODO deal damage
        }
    }
}
