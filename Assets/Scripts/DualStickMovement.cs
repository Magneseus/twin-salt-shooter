using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class DualStickMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public bool applyLookRotationToTransform = true;

    [HideInInspector]
    public int playerId = 0;
    [HideInInspector]
    public Vector3 lookRotation;
    [HideInInspector]
    public Vector3 lookDirection;
    [HideInInspector]
    public bool disableMovement = false;

    private Rewired.Player rePlayer;
    private CharacterController cc;
    private float moveDirectionOffset;

    // Use this for initialization
    void Start ()
    {
        rePlayer = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        moveDirectionOffset = Camera.main.transform.rotation.eulerAngles.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (disableMovement)
            return;

        // Movement
        Vector3 moveVec = new Vector3
        {
            x = rePlayer.GetAxis("LStick Horizontal"),
            z = rePlayer.GetAxis("LStick Vertical")
        };

        moveVec = Quaternion.AngleAxis(moveDirectionOffset, Vector3.up) * moveVec;

        cc.Move(moveVec * moveSpeed * Time.deltaTime);


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
            __y += 45.0f + moveDirectionOffset;

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
