using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndState : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider collision)
    {
        if (this.gameObject.tag == "Player")
        {

        }
    }
}
