using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    
    private Rewired.Player rePlayer;

    // Use this for initialization
    void Start () {

        rePlayer = ReInput.players.GetPlayer(0);

    }
	
	// Update is called once per frame
	void Update () {
       
        if (rePlayer.GetButton("A Button"))
        {
            SceneManager.LoadScene("CharacterSelect");
        }
        
	}
    

}
