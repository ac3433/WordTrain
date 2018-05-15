using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    AbstractAPI api;
    public string word;
    void Start()
    {
        api = WordsAPI.Instance;
        if (!string.IsNullOrEmpty(word))
        {
            api.SendWordToAPI(word);
            StartCoroutine(ex());
        }
    }

    IEnumerator ex()
    {
        while (api.InProgress)
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
