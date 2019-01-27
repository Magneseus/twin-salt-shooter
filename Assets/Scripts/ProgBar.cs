using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgBar : MonoBehaviour {

    public Slider slide;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void 
        SetSlider(float newVal)
    {
        slide.value = newVal;
    }

}
