using UnityEngine;
using System.Collections;

public class WordMuseAPI : AbstractAPI
{

    private void Awake()
    {
        BaseURL = "https://api.datamuse.com/";
    }

    public override void GetDefinition(string word)
    {
        throw new System.NotImplementedException();
    }

    public override void IsValid(string word)
    {
        string newURL = string.Format("words?sp={0}",word);
        InProgress = true;
        StartCoroutine(Validity(newURL));
    }

    private IEnumerator Validity(string url)
    {
        using (WWW req = new WWW(url))
        {
            while (!req.isDone)
                yield return req;

            if (string.IsNullOrEmpty(req.error))
            {
                System.Text.StringBuilder headerBuilder = new System.Text.StringBuilder();
                if (req.responseHeaders.Count > 0)
                {
                    string status = req.responseHeaders["STATUS"];
                    if (status.Contains("200"))
                        Valid = true;
                }
            }

            InProgress = false;
        }
    }
}
