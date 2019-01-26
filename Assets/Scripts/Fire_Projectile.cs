using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Projectile : PlayerUsable {

    public GameObject bullet;

    private PlayerScript player;


    public void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    public override void Use()
    {
        GameObject newBullet;
        newBullet = Instantiate(bullet, useLocation.transform.position, player.transform.rotation);
        newBullet.transform.rotation = player.transform.rotation;
        newBullet.GetComponent<Projectile>().SetPlayerNumber(player.playerId);
    }
}
