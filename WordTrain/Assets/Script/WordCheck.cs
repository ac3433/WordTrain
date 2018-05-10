using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCheck : MonoBehaviour {

    private AbstractAPI api;
	string word = "";

	// Use this for initialization
	void Start () {
        api = APIFactory.API();
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
                api.IsValid(word);
				StartCoroutine(ex());
			}

		}
		
	}

	IEnumerator ex()
	{
		while(api.InProgress)
		{
			yield return new WaitForSecondsRealtime(1);
		}

		//execute method based on the validity
		if (api.Valid)
		{
			Debug.LogFormat("The word {0} is valid", word);
		}
		else
		{
			Debug.LogFormat("The word {0} is invalid", word);
		}

        api.Reset();
	}
}
