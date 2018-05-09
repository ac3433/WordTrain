using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public string word;
    void Start()
    {
        if (!string.IsNullOrEmpty(word))
        {
            OxfordAPI.Instance.IsValid(word);
            StartCoroutine(ex());
        }
    }

    // Update is called once per frame
    void Update()
    {

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
