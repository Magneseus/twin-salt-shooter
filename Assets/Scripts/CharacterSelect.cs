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
    public GameObject[] hats;
    public GameObject[] guys;
    public GameObject[] hatPos;

    private Material[] guysMat;
    private GameObject[] currentHatObject;

    private int[] currentColor;
    private int[] currentHat;
    private int colorArraySize;
    private int hatArraySize;
    private bool[] isPlayerLockedIn;
    public Text countDownText;
    public float deadZone;

    private bool countDown;
    private float countDownTimer;

    // Use this for initialization
    void Start()
    {
        countDown = false;
        countDownTimer = 5;
        //p1 = ReInput.players.GetPlayer(0);
        currentColor = new int[4];
        currentHat = new int[4];
        p = new Rewired.Player[4];
        joystickControlHor = new bool[4];
        joystickControlVert = new bool[4];
        xControl = new bool[4];
        isPlayerLockedIn = new bool[4];
        countDownText.gameObject.SetActive(false);
        currentHatObject = new GameObject[4];

        guysMat = new Material[4];
        for (int i = 0; i < 4; i++)
        {
            currentColor[i] = i;
            currentHat[i] = i;
            guysMat[i] = guys[i].GetComponent<Renderer>().materials[0];
            guysMat[i].color = colors[currentColor[i]];

            currentHatObject[i] = Instantiate(hats[i], hatPos[i].transform.position, Quaternion.identity, hatPos[i].transform);
            currentHatObject[i].GetComponentInChildren<Renderer>().material.color = colors[i];

            p[i] = ReInput.players.GetPlayer(i);
            joystickControlHor[i] = true;
            joystickControlVert[i] = true;
            xControl[i] = true;
            isPlayerLockedIn[i] = false;
        }
        colorArraySize = colors.Length-1;
        hatArraySize = hats.Length-1;
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
                CheckJoystickVer(i);
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
        if (p[playerId].GetAxis("LStick Horizontal") < -deadZone || p[playerId].GetAxis("LStick Horizontal") > deadZone)
        {
            if (joystickControlHor[playerId])
            {
                joystickControlHor[playerId] = false;
                if(p[playerId].GetAxis("LStick Horizontal") < -deadZone)
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

    private void CheckJoystickVer(int playerId)
    {
        if (p[playerId].GetAxis("LStick Vertical") < -deadZone || p[playerId].GetAxis("LStick Vertical") > deadZone)
        {
            if (joystickControlVert[playerId])
            {
                joystickControlVert[playerId] = false;
                if (p[playerId].GetAxis("LStick Vertical") < -deadZone)
                {
                    ChangeHat(false, playerId);
                }
                else
                {
                    ChangeHat(true, playerId);
                }
            }
        }
        else
        {
            joystickControlVert[playerId] = true;
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
                currentHatObject[playerId].GetComponentInChildren<Renderer>().material.color = colors[currentColor[playerId]];
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

    //HATS HATS HATS

    private void ChangeHat(bool isLeft, int playerId)
    {
        int addNum = 1;
        addNum = isLeft ? addNum * -1 : addNum;
        int newHatToCheck = currentHat[playerId];
        bool newHatSelected = false;

        while (!newHatSelected)
        {
            newHatToCheck += addNum;
            if (newHatToCheck > hatArraySize)
            {
                newHatToCheck = 0;
            }
            else if (newHatToCheck < 0)
            {
                newHatToCheck = hatArraySize;
            }

            if (CheckIfHatFree(newHatToCheck))
            {
                Destroy(currentHatObject[playerId]);
                currentHatObject[playerId] = Instantiate(hats[newHatToCheck], hatPos[playerId].transform.position, Quaternion.identity, hatPos[playerId].transform);
                currentHat[playerId] = newHatToCheck;
                currentHatObject[playerId].GetComponentInChildren<Renderer>().material.color = colors[currentColor[playerId]];
                newHatSelected = true;
            }
        }
    }
    

    private bool CheckIfHatFree(int num)
    {
        bool isFree = true;
        for (int i = 0; i < 4; i++)
        {
            if (currentHat[i] == num)
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
        }
    }
}
