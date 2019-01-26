using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{
    public int playerId = 0;

    private Dictionary<string, PlayerUsable> actionMap;
    private Rewired.Player rePlayer;
    private CharacterController cc;


	// Use this for initialization
	void Start ()
    {
        actionMap = new Dictionary<string, PlayerUsable>();
        rePlayer = ReInput.players.GetPlayer(playerId);
        cc = GetComponent<CharacterController>();


        // ADD CONTROLS HERE
        actionMap.Add("RBumper", GetComponent<Fire_Projectile>());
        

        // Assign Use Locations if they exist
        foreach (KeyValuePair<string, PlayerUsable> action in actionMap)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                if (t.gameObject.name == "UseLocation" + action.Key)
                {
                    action.Value.useLocation = t.gameObject;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        DoInput();
	}

    private void DoInput()
    {
        foreach (KeyValuePair<string, PlayerUsable> action in actionMap)
        {
            if (rePlayer.GetButton(action.Key))
            {
                action.Value.Use();
            }
        }
    }
}
