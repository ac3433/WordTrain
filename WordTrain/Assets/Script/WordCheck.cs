using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCheck : MonoBehaviour {

    private AbstractAPI api;
	string word = "";

    private delegate void APIRequest(string word);

	// Use this for initialization
	void Start () {
        api = WordsAPI.Instance;
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
                FillAPIData(word);


            }

		}
		
	}

    public void FillAPIData(string word)
    {
        api.Reset();
        api.SendWordToAPI(word);
        StartCoroutine(ex());
    }

	IEnumerator ex()
	{
		while(api.InProgress)
		{
			yield return new WaitForSeconds(.3f);
		}

		//execute method based on the validity
		if (api.Valid)
		{
			Debug.LogFormat("The word {0} is valid", word);
            Debug.LogFormat("The word '{0}' has {1} syllable(s)", word, api.Syllable);
		}
		else
		{
			Debug.LogFormat("The word {0} is invalid", word);
		}

        api.Reset();
	}
}
