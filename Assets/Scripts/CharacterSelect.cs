using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

    private bool[] joystickControl;
    private Rewired.Player[] p;
    public Color[] colors;
    public GameObject[] guys;
    private Material[] guysMat;

    // Use this for initialization
    void Start()
    {
        
        //p1 = ReInput.players.GetPlayer(0);
        p = new Rewired.Player[4];
        joystickControl = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            guys[i].GetComponent<Renderer>().materials[3].color = colors[i];
            p[i] = ReInput.players.GetPlayer(i);
            joystickControl[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            CheckJoystick(i);
        }
    }


    private void CheckJoystick(int playerId)
    {
        if (p[playerId].GetAxis("LStick Horizontal") < -0.05 || p[playerId].GetAxis("LStick Horizontal") > 0.05)
        {
            if (joystickControl[playerId])
            {
                joystickControl[playerId] = false;
                //move left -right
            }
        }
        else
        {
            joystickControl[playerId] = true;
        }
    }
}
