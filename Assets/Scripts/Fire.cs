using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public GameObject bullet;
    public GameObject fireLocation;

    private PlayerScript player;


    public void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    public void FireBullet()
    {
        GameObject newBullet;
        newBullet = Instantiate(bullet, fireLocation.transform.position, player.transform.rotation);
        newBullet.transform.rotation = player.transform.rotation;
        newBullet.GetComponent<Projectile>().SetPlayerNumber(player.playerId);
    }
}
