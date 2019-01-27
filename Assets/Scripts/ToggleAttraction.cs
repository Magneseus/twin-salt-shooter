using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAttraction : PlayerUsable
{
    private PlayerScript player;
    private float lastUseTime;
    public float cd = 0;

    // Use this for initialization
    void Start () {
        player = GetComponent<PlayerScript>();
        lastUseTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Use()
    {
        if(lastUseTime + cd < Time.time)
        {
            player.gameObject.GetComponent<Leader>().ToggleLead();

            lastUseTime = Time.time;
        }
    }
}
