using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    private List<Boid> nearby;
    private float boid_radius;
    public float weight = 1.0f;
    public bool move = false;
    public float cohesionDivisor = 25.0f;
    public float alignDivisor = 8.0f;
    public float seperationDivisor = 8.0f;

    public Leader leader;
    private Rigidbody rb;

    public bool selected = false;

    void Awake()
    {
        nearby = new List<Boid>();
    }

	// Use this for initialization
	void Start () {
        boid_radius = 1;
        SphereCollider[] sc = GetComponentsInChildren<SphereCollider>();
        foreach(SphereCollider sc1 in sc)
        {
            if(sc1.isTrigger)
            {
                boid_radius = sc1.radius;
            }
        }

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

		if(move)
        {
            rb.velocity = new Vector3(10, 0, 0);
            move = false;
        }

        rb.velocity += GetVelocity();
        LimitVelocity();
        gameObject.transform.LookAt(rb.velocity + gameObject.transform.position);
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

        if (leader != null)
        {
            if (Vector3.Distance(rb.position, leader.gameObject.transform.position) > 1)
            {
                return GoToTarget(rb.position, leader.gameObject.transform.position) + separation / 4.0f;
            }
        }

        center = (center / nearby.Count) - rb.position;
        Vector3 add = center/cohesionDivisor + crowdDirection/alignDivisor + separation/seperationDivisor;

        if ((rb.velocity + add).sqrMagnitude < 4)
        {
            return add;
        }

        return new Vector3(0, 0, 0);
    }

    public Vector3 GoToTarget(Vector3 loc, Vector3 targ)
    {
        Vector3 towards = new Vector3();

        towards = targ - loc;
        float dist = Vector3.Distance(targ, loc);
        towards = towards.normalized * (Mathf.Pow(dist - 10, 2) / 5);

        return towards;
    }

    public void LimitVelocity()
    {
        if(rb.velocity.sqrMagnitude > 10)
        {
            rb.velocity = rb.velocity.normalized * 10.0f;
        }
    }

    public void LoseLeader()
    {
        leader = null;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void OnTriggerEnter(Collider c)
    {
        if(c.gameObject == this.gameObject || c.isTrigger)
            return;

        Boid b = c.gameObject.GetComponentInChildren<Boid>();
        if(b != null)
        {
            nearby.Add(b);
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
        }
    }
    
}
