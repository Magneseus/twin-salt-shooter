using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerInteractable : MonoBehaviour
{
    public virtual bool IsBlocking { get; set; }
    public virtual float PercentComplete { get; set; }

    public virtual void Interact()
    {

    }
}
