using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerInteractable : MonoBehaviour
{
    public bool IsComplete { get { return PercentComplete == 100.0f; } }

    public bool IsBlocking = false;
    public float PercentComplete = 0.0f; // 0-100
    public float SecondsToComplete = 0.0f;
    public float PercentDecayPerSecond = 0.0f;

    protected Collider intCollider;
    protected bool IsInteracting = false;
    

    public virtual void Interact()
    {
        IsInteracting = true;
    }

    public virtual void StopInteract()
    {
        IsInteracting = false;
    }

    public virtual void OnComplete()
    {

    }

    private void Update()
    {
        if (IsInteracting)
        {
            PercentComplete += (Time.deltaTime / SecondsToComplete) * 100.0f;

            if (PercentComplete >= 100.0f)
            {
                PercentComplete = 100.0f;
                OnComplete();
            }
        }
        else
        {
            if (PercentDecayPerSecond > 0.0f && PercentComplete > 0.0f)
            {
                PercentComplete -= PercentDecayPerSecond * Time.deltaTime;

                if (PercentComplete < 0.0f)
                {
                    PercentComplete = 0.0f;
                }
            }
        }
    }

    private void Start()
    {
        intCollider = GetComponent<Collider>();
        if (intCollider == null)
        {
            Debug.Log(transform.gameObject.name + ": This Interactable will not work! No Collider attached!");
        }
        if (!intCollider.isTrigger)
        {
            Debug.Log(transform.gameObject.name + ": This Interactable will not work! Collider is not a trigger!");
        }
        if (transform.tag != "Interactable")
        {
            Debug.Log(transform.gameObject.name + ": This Interactable will not work! Tag not set to 'Interactable'!");
        }
    }
}
