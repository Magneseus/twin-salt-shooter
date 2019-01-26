using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedDoor : PlayerInteractable
{
    public GameObject doorObject;

    public override void OnComplete()
    {
        doorObject.SetActive(false);
    }
}
