using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {

    private bool[] joystickControlHor;
    private bool[] joystickControlVert;
    private bool[] xControl;
    private Rewired.Player[] p;
    public Color[] colors;
    public GameObject[] guys;
    private Material[] guysMat;
    private int[] currentColor;
    private int colorArraySize;
    private bool[] isPlayerLockedIn;
    public Text countDownText;

    private bool countDown;
    private float countDownTimer;

    // Use this for initialization
    void Start()
    {
        countDown = false;
        countDownTimer = 5;
        //p1 = ReInput.players.GetPlayer(0);
        currentColor = new int[4];
        p = new Rewired.Player[4];
        joystickControlHor = new bool[4];
        joystickControlVert = new bool[4];
        xControl = new bool[4];
        isPlayerLockedIn = new bool[4];
        countDownText.gameObject.SetActive(false);

        guysMat = new Material[4];
        for (int i = 0; i < 4; i++)
        {
            currentColor[i] = i;
            guysMat[i] = guys[i].GetComponent<Renderer>().materials[3];
            guysMat[i].color = colors[currentColor[i]];
            p[i] = ReInput.players.GetPlayer(i);
            joystickControlHor[i] = true;
            joystickControlVert[i] = true;
            xControl[i] = true;
            isPlayerLockedIn[i] = false;
        }
        colorArraySize = colors.Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        //quick key to auto confirm everyone and start the game
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1))
        {
            for (int i = 0; i < 4; i++)
            {
                isPlayerLockedIn[i] = true;
            }
        }

        //check if all players are confirmed
        bool allConfirmed = true;
        for (int i = 0; i < 4; i++)
        {
            if (!isPlayerLockedIn[i])
            {
                //check for joystick input
                CheckJoystickHor(i);
            }
            //check for x button
            CheckConfirmButton(i);
            if (!isPlayerLockedIn[i])
            {
                allConfirmed = false;
            }
        }

        //if all have confirmed, start countdown
        if (allConfirmed)
        {
            if (!countDown)
            {
                countDown = true;
                countDownTimer = 5;
                countDownText.gameObject.SetActive(true);
            }
        }
        //if not everyone ready stop countdown
        else
        {
            countDown = false;
            countDownTimer = 5;
            countDownText.gameObject.SetActive(false);

        }
        //countdown to start the game
        if (countDown)
        {
            countDownTimer -= Time.deltaTime;
            countDownText.text = "Game Starting in: " + Mathf.RoundToInt(countDownTimer);

            if(countDownTimer < 0)
            {
                //TODO load main scene
                print("Game has Started");
            }
        }
    }


    private void CheckJoystickHor(int playerId)
    {
        if (p[playerId].GetAxis("LStick Horizontal") < -0.05 || p[playerId].GetAxis("LStick Horizontal") > 0.05)
        {
            if (joystickControlHor[playerId])
            {
                joystickControlHor[playerId] = false;
                if(p[playerId].GetAxis("LStick Horizontal") < -0.05)
                {
                    ChangeColor(false, playerId);
                }
                else
                {
                    ChangeColor(true, playerId);
                }
            }
        }
        else
        {
            joystickControlHor[playerId] = true;
        }
    }

    private void ChangeColor(bool isLeft, int playerId)
    {
        int addNum = 1;
        addNum = isLeft ? addNum * -1 : addNum;
        int newColorToCheck = currentColor[playerId];
        bool newColorSelected = false;

        while (!newColorSelected)
        {
            newColorToCheck += addNum;
            if (newColorToCheck > colorArraySize)
            {
                newColorToCheck = 0;
            }
            else if (newColorToCheck < 0)
            {
                newColorToCheck = colorArraySize;
            }

            if (CheckIfNumberFree(newColorToCheck))
            {
                currentColor[playerId] = newColorToCheck;
                guysMat[playerId].color = colors[currentColor[playerId]];
                newColorSelected = true;
            }
        }
    }

    private bool CheckIfNumberFree(int num)
    {
        bool isFree = true;
        for(int i = 0; i < 4; i++)
        {
            if(currentColor[i] == num)
            {
                isFree = false;
            }
        }
        return isFree;
    }

    private void CheckConfirmButton(int playerId)
    {
        
        if(p[playerId].GetButtonDown("A Button"))
        {
            isPlayerLockedIn[playerId] = isPlayerLockedIn[playerId] ? false : true;
            //print("player: " + playerId + " is confirmed?: " + isPlayerLockedIn[playerId]);
        }

        

    }
}
