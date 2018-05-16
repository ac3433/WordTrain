using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour {

    public Vector3 newPos;
    Vector3 oldPos;
    bool toggled = false;
    RectTransform rectTransform;

	// Use this for initialization
	void Start () {
        rectTransform = gameObject.GetComponent<RectTransform>();
        oldPos = rectTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle()
    {
        if (toggled)
        {
            rectTransform.localPosition = oldPos;
            toggled = false;
        } else
        {
            rectTransform.localPosition += newPos;
            toggled = true;
        }
    }
}
