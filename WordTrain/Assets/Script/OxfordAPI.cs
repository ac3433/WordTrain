using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OxfordAPI : AbstractAPI
{
    void Awake()
    {
        Headers = new Dictionary<string, string>();

        BaseURL = "https://od-api.oxforddictionaries.com/api/v1/";
        Key = "54c3297ec769a7233b81ec5a1b68ac67";
        App_Id = "34755312";

        Headers["Accept"] = "application/json";
        Headers["app_id"] = App_Id;
        Headers["app_key"] = Key;
    }

    public override void IsValid(string word)
    {
        string newURL = BaseURL + "inflections/en/" + word;
        InProgress = true;
        StartCoroutine(Validity(newURL));
    }

    private IEnumerator Validity(string url)
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
                        Valid = true;
                }
            }

            InProgress = false;
        }
    }

    public override void GetDefinition(string word)
    {
        throw new System.NotImplementedException();
    }
}
