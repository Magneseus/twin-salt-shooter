using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool isCross;
    private Vector3 randRotate;
    public GameObject childObj;
    public GameObject explosion;
    public float speedRot;
    public float maxLifeTime;
    private float currentLife;
    public float speed;
    private int playerThatFiredMe;
    public float rotationOffset;
    
	// Use this for initialization
	void Start () {
        currentLife = 0;
        randRotate = new Vector3(Random.Range(-speedRot, speedRot), Random.Range(-speedRot, speedRot), Random.Range(-speedRot, speedRot));
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


        if (isCross)
        {
            childObj.transform.localEulerAngles += randRotate;
        }

	}

    public void SetPlayerNumber(int num)
    {
        playerThatFiredMe = num;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "structural")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy" && isCross)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

   

}
