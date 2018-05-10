using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    AbstractAPI api;
    public string word;
    void Start()
    {
        api = APIFactory.API();
        if (!string.IsNullOrEmpty(word))
        {
            api.IsValid(word);
            StartCoroutine(ex());
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
