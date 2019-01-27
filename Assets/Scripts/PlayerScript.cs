using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{
    
    public int playerId = 0;
    public string interactButton = "X Button";
    public GameObject playerObject;
    public GameObject handObject;
    public GameObject gun;
    public GameObject crossbowObj;
    public GameObject throwerObj;
    private Color playerColor;
    public Renderer ObjectThatNeedsToChangeColor;
    public GameObject hatPosition;
    private GameObject hat;

    private Dictionary<string, PlayerUsable> actionMap;
    private List<PlayerInteractable> interactables;

    private bool inputBlocked = false;
    private float startingY = 0.0f;

    private Rewired.Player rePlayer;
    private CharacterController cc;
    private DualStickMovement movement;
    public enum Weapon { pistol, flameThrower, crossbow};
    public Weapon currentWeapon;

    // Use this for initialization
    void Start()
    {
        currentWeapon = Weapon.pistol;
        actionMap = new Dictionary<string, PlayerUsable>();
        interactables = new List<PlayerInteractable>();
        rePlayer = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();
        movement = GetComponent<DualStickMovement>();

        startingY = transform.position.y;

        // ADD CONTROLS HERE
        if(currentWeapon == Weapon.flameThrower)
        {
            actionMap.Add("RBumper", GetComponent<FlameThrower>());//GetComponent<Fire_Projectile>());
        }
        else if (currentWeapon == Weapon.pistol)
        {
            actionMap.Add("RBumper", GetComponent<Fire_Projectile>());
        }
        else if (currentWeapon == Weapon.crossbow)
        {
            //actionMap.Add("RBumper", GetComponent<FlameThrower>());//GetComponent<Fire_Projectile>());
        }
            

        // Assign Use Locations if they exist
        foreach (KeyValuePair<string, PlayerUsable> action in actionMap)
        {
            GameObject go;
            if (FindChildWithName(transform, action.Key, out go))
            {
                action.Value.useLocation = go;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        DoInput();
        DoInteractables();
        if(currentWeapon == Weapon.pistol)
        {
            gun.SetActive(true);
            crossbowObj.SetActive(false);
            throwerObj.SetActive(false);
            
            actionMap.Remove("RBumper");
            actionMap.Add("RBumper", GetComponent<Fire_Projectile>());
        }
        else if(currentWeapon == Weapon.crossbow)
        {
            crossbowObj.SetActive(true);
            throwerObj.SetActive(false);

            gun.SetActive(false);
            actionMap.Remove("RBumper");
            actionMap.Add("RBumper", GetComponent<Fire_Projectile>());

        }
        else
        {
            crossbowObj.SetActive(false);
            throwerObj.SetActive(true);

            gun.SetActive(false);
            actionMap.Remove("RBumper");
            actionMap.Add("RBumper", GetComponent<FlameThrower>());

        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
    }

    private void DoInput()
    {
        if (inputBlocked)
            return;

        if(rePlayer.GetAxis("LStick Horizontal") != 0)
        {
            float newY = Mathf.Sin(Time.time*10)/10;
            playerObject.transform.localPosition = new Vector3(playerObject.transform.localPosition.x, newY, playerObject.transform.localPosition.z);
            float newX = Mathf.Sin(Time.time * 10) / 400;
            handObject.transform.localPosition = new Vector3(newX, handObject.transform.localPosition.y, handObject.transform.localPosition.z);

        }

        // Activate all actions if they exist
        foreach (KeyValuePair<string, PlayerUsable> action in actionMap)
        {
            if (rePlayer.GetButton(action.Key))
            {
                action.Value.Use();
            }
            else if (rePlayer.GetButtonUp(action.Key))
            {
                action.Value.UnUse();
            }
            else if (rePlayer.GetButtonDown(action.Key))
            {
                action.Value.UseOnce();
            }
        }
    }

    private void DoInteractables()
    {
        // If inside an interactable box, can activate with the 'interactButton'
        // Will disable other controls if IsBlocking
        List<PlayerInteractable> interactablesToRemove = new List<PlayerInteractable>();
        foreach (PlayerInteractable intScript in interactables)
        {
            // If we're interacting, start the interact and check for blocking
            if (rePlayer.GetButton(interactButton))
            {
                intScript.Interact(this.gameObject);

                if (intScript.IsBlocking)
                {
                    movement.disableMovement = true;
                    inputBlocked = true;
                }
            }
            // If we're not, stop interact and stop blocking
            else
            {
                intScript.StopInteract(this.gameObject);

                movement.disableMovement = false;
                inputBlocked = false;
            }

            // If Complete remove from list and stop blocking
            if (intScript.IsComplete)
            {
                interactablesToRemove.Add(intScript);
                movement.disableMovement = false;
                inputBlocked = false;
            }
        }

        // Remove complete interactables
        foreach(PlayerInteractable i in interactablesToRemove)
        {
            interactables.Remove(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            PlayerInteractable inter = other.GetComponent<PlayerInteractable>();

            if (!inter.IsComplete)
            {
                interactables.Add(inter);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            interactables.Remove(other.GetComponent<PlayerInteractable>());
            movement.disableMovement = false;
            inputBlocked = false;
        }
    }

    private bool FindChildWithName(Transform t, string action, out GameObject go)
    {
        go = null;
        
        for (int i = 0; i < t.childCount; i++)
        {
            Transform __t = t.GetChild(i);
            if (__t.gameObject.name == "UseLocation" + action)
            {
                go = __t.gameObject;
                return true;
            }
            else
            {
                if (FindChildWithName(__t, action, out go))
                {
                    return true;
                }
            }
        }

        return false;
    }


    public void SetColor(Color col)
    {
        playerColor = col;
        ObjectThatNeedsToChangeColor.material.color = col;
    }

    public void SetHat(GameObject newHat)
    {
        hat = Instantiate(newHat, hatPosition.transform.position, Quaternion.identity, hatPosition.transform);
        hat.GetComponentInChildren<Renderer>().material.color = playerColor;
        Vector3 rot = hat.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        hat.transform.rotation = Quaternion.Euler(rot);
        hat.transform.localScale = new Vector3(1 / transform.localScale.x, 1 / transform.localScale.y, 1 / transform.localScale.z);
    }
    
    public void ChangeWeapons(string type)
    {
        if(type == "pistol")
        {
            currentWeapon = Weapon.pistol;
        }else if(type == "flameThrower")
        {
            currentWeapon = Weapon.flameThrower;
        }
        else if (type == "crossbow")
        {
            currentWeapon = Weapon.crossbow;
        }
    }

    public string GetWeapon()
    {
        if (currentWeapon == Weapon.pistol)
        {
            return "pistol";
        }
        else if (currentWeapon == Weapon.crossbow)
        {
            return "crossbow";
        }
        else
        {
            return "flameThrower";
        }
    }

}
