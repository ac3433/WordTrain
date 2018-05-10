using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCheck : MonoBehaviour {

	string word = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Space))
		{
			word = "";
			for(int i = 0; i < this.transform.childCount; i++){
				word = word + this.transform.GetChild(i).GetComponent<CardControl> ().letter;
			}

			if (!string.IsNullOrEmpty(word))
			{
				OxfordAPI.Instance.IsValid(word);
				StartCoroutine(ex());
			}

		}
		
	}

	IEnumerator ex()
	{
		while(OxfordAPI.Instance.InProgress)
		{
			yield return new WaitForSecondsRealtime(1);
		}

		//execute method based on the validity
		if (OxfordAPI.Instance.Valid)
		{
			Debug.LogFormat("The word {0} is valid", word);
		}
		else
		{
			Debug.LogFormat("The word {0} is invalid", word);
		}

		OxfordAPI.Instance.Reset();
	}
}
