using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {

    private List<Boid> followers;
    private bool leading = true;

	// Use this for initialization
	void Start () {
        followers = new List<Boid>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleLead()
    {
        if(leading)
        {
            Debug.Log("STOPPING");
            StopLeading();
        }
        else
        {
            Debug.Log("STARTING");
            StartLeading();
        }
    }

    public void StartLeading()
    {
        leading = true;

        Collider[] c = gameObject.GetComponents<Collider>();
        foreach (Collider c1 in c)
        {
            if (c1.isTrigger)
                c1.enabled = true;
        }
    }

    public void StopLeading()
    {
        foreach(Boid b in followers)
        {
            b.LoseLeader();
        }

        leading = false;
        Collider[] c = gameObject.GetComponents<Collider>();
        foreach(Collider c1 in c)
        {
            if (c1.isTrigger)
                c1.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        Boid b = c.gameObject.GetComponent<Boid>();

        if (b != null)
        {
            b.leader = this;
            followers.Add(b);
        }
    }

    public void OnTriggerExit(Collider c)
    {
        Boid b = c.gameObject.GetComponent<Boid>();
        if (b != null)
        {
            b.LoseLeader();
            followers.Remove(b);
        }
    }
}
