using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoor : PlayerInteractable {

    public float RotateAmount = 90.0f;
    public float RotateInSecs = 3.0f;
    
    private bool rotating = false;
    private float startTime;
    private Quaternion startRot;
    private Quaternion endRot;

    private void Start()
    {
        endRot = Quaternion.identity;
        endRot.SetFromToRotation(new Vector3(0, 0, 0), new Vector3(0, RotateAmount, 0));
    }

    public override void OnComplete()
    {
        rotating = true;
        startRot = transform.rotation;
        startTime = Time.time;
    }

    private void Update()
    {
        base.Update();
        if (rotating)
        {
            transform.rotation = Quaternion.Slerp(
                startRot,
                endRot,
                1.0f - ((Time.time - startTime) / RotateInSecs));
        }
    }
}
