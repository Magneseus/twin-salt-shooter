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

    private void Start()
    {
        if (IsBroken)
            SecondsToComplete = PlayerSecondsToBuild;
        else
            SecondsToComplete = EnemySecondsToBreak;
    }


    public override void Interact(GameObject interactor)
    {
        if (IsBroken && interactor.tag == "Player")
        {
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
