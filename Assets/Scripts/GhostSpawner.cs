using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour {
    public int numGhostsPerSecond = 1;
    public GameObject ghost;
    public float timeSinceSpawn = 0;
    private float startTime;
    public float respawnTime;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        startTime += Time.deltaTime;
		if(startTime > respawnTime)
        {
            startTime = 0;
            print(startTime);
            GameObject newGhost;
            for (int i = 0; i < numGhostsPerSecond; i++)
            {
                newGhost = Instantiate(ghost);
                newGhost.transform.SetPositionAndRotation(new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), Quaternion.identity);
                newGhost.GetComponent<Boid>().MoveToPlayer();
            }
            
        }
	}
}
