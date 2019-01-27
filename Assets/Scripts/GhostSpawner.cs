using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour {
    public int numGhosts = 5;
    public GameObject ghost;

	// Use this for initialization
	void Start () {
        GameObject newGhost;
		for(int i=0; i<numGhosts; i++)
        {
            newGhost = Instantiate(ghost);
            newGhost.transform.SetPositionAndRotation(new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
            newGhost.GetComponent<Boid>().move = Random.Range(0, 100) < 10;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
