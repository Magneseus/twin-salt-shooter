using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : PlayerInteractable {


    public enum Weapon { pistol, flameThrower, crossbow};
    public Weapon currentWeapon;
    public GameObject[] weaponObj;
    public float coolDownTime;
    private float currentTime;
    private bool allowUse;

    // Use this for initialization
    void Start () {
        allowUse = true;
        if (currentWeapon == Weapon.pistol)
        {
            weaponObj[0].SetActive(true);
            weaponObj[1].SetActive(false);
            weaponObj[2].SetActive(false);
        }
        else if (currentWeapon == Weapon.flameThrower)
        {
            weaponObj[0].SetActive(false);
            weaponObj[1].SetActive(true);
            weaponObj[2].SetActive(false);
        }
        else if (currentWeapon == Weapon.crossbow)
        {
            weaponObj[0].SetActive(false);
            weaponObj[1].SetActive(false);
            weaponObj[2].SetActive(true);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime < coolDownTime)
        {
            allowUse = false;
        }
        else
        {
            allowUse = true;
        }
    }

	public override void Interact(GameObject interact)
    {
        if (allowUse)
        {
            allowUse = false;
            currentTime = 0;

            PlayerScript pS = interact.GetComponent<PlayerScript>();


            if (currentWeapon == Weapon.pistol)
            {
                checkWeapon(interact);
                pS.ChangeWeapons("pistol");
            }
            else if (currentWeapon == Weapon.crossbow)
            {
                checkWeapon(interact);
                pS.ChangeWeapons("crossbow");
            }
            else if (currentWeapon == Weapon.flameThrower)
            {
                checkWeapon(interact);
                pS.ChangeWeapons("flameThrower");
            }


        }

    }
    void checkWeapon(GameObject interact)
    {
        PlayerScript pS = interact.GetComponent<PlayerScript>();

        if (pS.GetWeapon() == "pistol")
        {
            currentWeapon = Weapon.pistol;
            weaponObj[0].SetActive(true);
            weaponObj[1].SetActive(false);
            weaponObj[2].SetActive(false);

        }
        else if (pS.GetWeapon() == "flameThrower")
        {
            currentWeapon = Weapon.flameThrower;
            weaponObj[0].SetActive(false);
            weaponObj[1].SetActive(true);
            weaponObj[2].SetActive(false);

        }
        else if (pS.GetWeapon() == "crossbow")
        {
            currentWeapon = Weapon.crossbow;
            weaponObj[0].SetActive(false);
            weaponObj[1].SetActive(false);
            weaponObj[2].SetActive(true);
        }
    }
}
