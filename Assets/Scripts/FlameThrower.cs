using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : PlayerUsable {

    public GameObject damageArea;

    private Rewired.Player rePlayer;

    // Use this for initialization
    void Start () {
        damageArea.SetActive(false);
    }
    public override void Use()
    {
        damageArea.SetActive(true);
    }

    public override void UnUse()
    {
        damageArea.SetActive(false);
    }
}
