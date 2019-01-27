using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour {
    public int numGhostsPerSecond = 10;
    public GameObject ghost;
    public float timeSinceSpawn = 0;
    private float startTime;
    public float respawnTime;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < numGhostsPerSecond; i++)
        {
            GameObject newGhost = Instantiate(ghost, transform.position + new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
            //newGhost.transform.SetPositionAndRotation(new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
            newGhost.GetComponent<Boid>().MoveToPlayer();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
            
	}
}
