using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{
    public int playerId = 0;

    private Rewired.Player rePlayer;
    private CharacterController cc;

	// Use this for initialization
	void Start ()
    {
        rePlayer = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
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
    }
}
