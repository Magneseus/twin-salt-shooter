using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{
    public int playerId = 0;

    private Rewired.Player rePlayer;
    private CharacterController cc;
    private Fire_Projectile firescript;

	// Use this for initialization
	void Start ()
    {
        rePlayer = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        firescript = GetComponent<Fire_Projectile>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DoInput();
	}

    private void DoInput()
    {
        bool fire = rePlayer.GetButton("RBumper");

        if (fire)
        {
            firescript.FireBullet();
        }
    }
}
