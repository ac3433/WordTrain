using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardControl : MonoBehaviour {

	public int value = 2;
	public string letter = "t";
	public Transform parent = null;
    public bool isLock;

	// Use this for initialization
	void Start () {
		parent = this.transform.parent;
        isLock = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDrag()
	{
		if(!isLock)
        {
            Vector3 moveTo = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            transform.position = new Vector3(moveTo.x, moveTo.y, transform.position.z);
            this.transform.SetParent(parent.parent);


        }

	}

	void OnMouseUp()
	{
        if(!isLock)
        {
            this.transform.SetParent(parent);

            foreach (Transform child in transform.parent)
            {
                Debug.Log(child.name);
                if (child.GetComponent<CardControl>().isLock)
                {
                    child.SetSiblingIndex(0);
                }
            }
        }

	}


}
