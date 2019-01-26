using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    private List<Boid> nearby;
    private float boid_radius;
    public float weight = 1.0f;
    public bool move = false;
    public float cohesionDivisor = 25;
    public float alignDivisor = 8;
    public float seperationDivisor = 8;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        nearby = new List<Boid>();

        boid_radius = 1;
        SphereCollider sc = GetComponent<SphereCollider>();
        if(sc != null)
        {
            boid_radius = sc.radius;
        }

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		if(move)
        {
            //rb.velocity = new Vector3(5, 0, 0);
            move = false;
        }
        rb.velocity += GetVelocity();
	}

    private Vector3 GetVelocity()
    {
        Vector3 crowdDirection = new Vector3(0, 0, 0);
        Vector3 center = new Vector3(0, 0, 0);
        Vector3 separation = new Vector3(0, 0, 0);
        float dist;
        Vector3 away;
        Rigidbody b_rb;
        foreach (Boid b in nearby)
        {
            b_rb = b.GetComponent<Rigidbody>();
            
            // converge
            center += b_rb.position * b.weight;

            // separate
            dist = Vector3.Distance(b_rb.position, gameObject.transform.position);
            if (dist < boid_radius / 2)
            {
                away = (b_rb.position - gameObject.transform.position);
                separation -= away.normalized * ((boid_radius / 2) - dist);
            }

            // mimic velocity
            crowdDirection += (b_rb.velocity / nearby.Count) * b.weight;
        }

        center = (center / nearby.Count) - rb.position;
        Vector3 add = center/cohesionDivisor + crowdDirection/alignDivisor + separation/seperationDivisor;

        if ((rb.velocity + add).sqrMagnitude < 4)
        {
            return add;
        }

        return new Vector3(0, 0, 0);
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject == this.gameObject || c.isTrigger)
            return;

        Boid b = c.gameObject.GetComponentInChildren<Boid>();
        if(b != null)
        {
            nearby.Add(b);

            Debug.Log("Added boid of " + c + " to " + this);
        }

    }

    public void OnTriggerExit(Collider c)
    {
        if (c.gameObject == this.gameObject || c.isTrigger)
            return;

        Boid b = c.gameObject.GetComponentInChildren<Boid>();
        if (b != null)
        {
            nearby.Remove(b);
            Debug.Log("Removed boid! " + nearby.Count + " elements left.");
        }
    }
}
