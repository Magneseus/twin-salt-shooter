using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class DualStickMovement : MonoBehaviour
{
    public int playerId = 0;
    public bool applyLookRotationToTransform = true;
    public Vector3 lookRotation;
    public Vector3 lookDirection;

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
        // Movement
        Vector3 moveVec = new Vector3
        {
            x = rePlayer.GetAxis("LStick Horizontal"),
            z = rePlayer.GetAxis("LStick Vertical")
        };

        cc.Move(moveVec * 3.0f * Time.deltaTime);


        // Rotation

        // Get stick direction
        Vector3 lookVector = new Vector3
        {
            x = rePlayer.GetAxis("RStick Horizontal") * -1,
            y = rePlayer.GetAxis("RStick Vertical")
        };

        if (lookVector.sqrMagnitude > 0)
        {
            // Convert look vector to an angle
            float __y = Vector2.SignedAngle((new Vector2(-0.5f, 0.5f)), lookVector);
            __y += 45.0f;

            // Set public facing vars
            lookDirection = new Vector3 { x = lookVector.x, y = lookVector.y };
            lookRotation = new Vector3 { y = __y };

            // If applying to transform
            if (applyLookRotationToTransform)
            {
                cc.transform.rotation = Quaternion.Euler(0, __y, 0);
            }
        }
    }
}
