using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public GameObject bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("Fire1"))
        {
            FireBullet();
        }
	}

    private void FireBullet()
    {
        int playerNum = Random.Range(1, 5);
        GameObject newBullet;
        newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.GetComponent<Projectile>().SetPlayerNumber(playerNum);
    }
}
