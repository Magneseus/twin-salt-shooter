using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : PlayerInteractable
{
    public GameObject barricadeObj;
    public GameObject barricadePhysical;
    private bool IsBroken = true;

    public float EnemySecondsToBreak;
    public float PlayerSecondsToBuild;

    private ParticleSystem ps;

    private void Start()
    {
        if (IsBroken)
            SecondsToComplete = PlayerSecondsToBuild;
        else
            SecondsToComplete = EnemySecondsToBreak;

        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }


    public override void Interact(GameObject interactor)
    {
        if (IsBroken && interactor.tag == "Player")
        {
            if(!IsInteracting)
                ps.Play();

            IsInteracting = true;
        }

        if (!IsBroken && interactor.tag == "Enemy")
        {
            IsInteracting = true;
        }
    }

    public override void StopInteract(GameObject interactor)
    {
        if (IsBroken && interactor.tag == "Player")
        {
            if (IsInteracting)
                ps.Stop();

            IsInteracting = false;
        }

        if (!IsBroken && interactor.tag == "Enemy")
        {
            IsInteracting = false;
        }
    }

    public override void OnComplete()
    {
        if (IsBroken)
        {
            ps.Stop();
            barricadeObj.SetActive(true);
            barricadePhysical.GetComponent<BoxCollider>().enabled = true;

            IsBroken = false;
            PercentComplete = 0;
            SecondsToComplete = EnemySecondsToBreak;
        }
        else
        {
            barricadeObj.SetActive(false);
            barricadePhysical.GetComponent<BoxCollider>().enabled = false;

            IsBroken = true;
            PercentComplete = 0;
            SecondsToComplete = PlayerSecondsToBuild;
        }
    }
}
