using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Projectile : PlayerUsable {

    public GameObject bullet;

    private PlayerScript player;

    public float cooldownMaxTime;
    private float countdownCurrent;


    public void Start()
    {
        countdownCurrent = 0;
        player = GetComponent<PlayerScript>();
    }

    public override void Use()
    {
        countdownCurrent += Time.deltaTime;

        if (countdownCurrent > cooldownMaxTime)
        {
            countdownCurrent = 0;
            GameObject newBullet;
            newBullet = Instantiate(bullet, useLocation.transform.position, player.transform.rotation);
            newBullet.transform.rotation = player.transform.rotation;
            newBullet.GetComponent<Projectile>().SetPlayerNumber(player.playerId);
        }
    }
}
