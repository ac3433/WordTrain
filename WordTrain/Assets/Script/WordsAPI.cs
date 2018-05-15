using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WordsAPI : AbstractAPI
{

    private void Awake()
    {
        Headers = new Dictionary<string, string>();

        BaseURL = "https://wordsapiv1.p.mashape.com/words/";
        Key = "BuL2HMkpEamshf6O8JWJHBPy3N0Gp17LJGbjsnyzi6HbRHjb0A";
        App_Id = "wordsapiv1.p.mashape.com";

        Headers["Accept"] = "application/json";
        Headers["X-Mashape-Key"] = Key;
        Headers["X-Mashape-Host"] = App_Id;
    }

    public override void PrintAPIName()
    {
        Debug.Log("WordAPI");
    }

    public override void SendWordToAPI(string word)
    {
        string newURL = BaseURL + word;
        InProgress = true;
        StartCoroutine(Request(newURL));
    }

    private IEnumerator Request(string url)
    {
        using (WWW req = new WWW(url, null, Headers))
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
                    {
                        Valid = true;
                        SetSyllable(req.text);

                    }
                }
            }
            InProgress = false;
        }
    }

    private void SetSyllable(string text)
    {
        int from = text.IndexOf("count\":") + "count\":".Length;
        int to = text.IndexOf(",\"list");

        string result = text.Substring(from, to - from);
        Syllable = Convert.ToInt32(result);
    }
}
