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
        Vector3 moveVec = new Vector3();
        moveVec.x = rePlayer.GetAxis("LStick Horizontal");
        moveVec.z = rePlayer.GetAxis("LStick Vertical");

        cc.Move(moveVec * 3.0f * Time.deltaTime);

        Vector3 lookVector = new Vector3();
        lookVector.x = rePlayer.GetAxis("RStick Horizontal") * -1;
        lookVector.y = rePlayer.GetAxis("RStick Vertical");

        if (lookVector.sqrMagnitude > 0)
        {
            float __y = Vector2.SignedAngle((new Vector2(-0.5f, 0.5f)), lookVector);
            __y += 45.0f;
            cc.transform.rotation = Quaternion.Euler(0, __y, 0);
        }

        bool fire = rePlayer.GetButton("RBumper");

        if (fire)
        {
            firescript.FireBullet();
        }
    }
}
