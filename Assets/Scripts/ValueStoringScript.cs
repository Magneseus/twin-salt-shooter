using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValueStoringScript : MonoBehaviour {

    
    private Color[] playerColor;
    private GameObject[] playerHat;
    bool setVariables;

	// Use this for initialization
	void Start () {
        setVariables = false;
        playerColor = new Color[4];
        playerHat = new GameObject[4];
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (!setVariables)
        {
            if (SceneManager.GetActiveScene().name == "GAME")
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach(GameObject player in players)
                {
                    PlayerScript pS = player.GetComponent<PlayerScript>();
                    pS.SetColor(playerColor[pS.playerId]);
                    pS.SetHat(playerHat[pS.playerId]);
                }

                if(players.Length > 0)
                {
                    setVariables = true;
                }

            }
        }
	}

    public void SetPlayerColor(int playerId, Color newColor)
    {
        playerColor[playerId] = newColor;
    }

    public void SetPlayerObject(int playerId, GameObject newHat)
    {
        playerHat[playerId] = newHat;
    }

    



}
