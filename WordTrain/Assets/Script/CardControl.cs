using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardControl : MonoBehaviour {

	public int value = 2;
	public string letter = "t";
	public Transform parent = null; 

	// Use this for initialization
	void Start () {
		parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag()
	{
		
		Vector3 moveTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		transform.position = new Vector3(moveTo.x, moveTo.y, transform.position.z);
		this.transform.SetParent (parent.parent);
	}

	void OnMouseUp()
	{
		this.transform.SetParent (parent);
	}


}
