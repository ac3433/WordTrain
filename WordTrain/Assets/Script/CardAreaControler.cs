using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAreaControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		other.GetComponent<CardControl>().parent = this.transform;
	}
}
