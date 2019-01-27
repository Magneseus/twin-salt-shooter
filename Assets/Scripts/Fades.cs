using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fades : MonoBehaviour {

    private float offs;
    private Renderer[] materials;

	// Use this for initialization
	void Start () {
        materials = GetComponentsInChildren<Renderer>();
        offs = Random.Range(0.0f, 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
        float t = Time.time + offs;
        
        Color c;
        
        foreach (Renderer r in materials)
        {
            c = r.material.color;
            c.a = (-Mathf.Pow(Mathf.Sin(t), 5) + 1)/4;
            
            r.material.color = c;
        }
	}
}
