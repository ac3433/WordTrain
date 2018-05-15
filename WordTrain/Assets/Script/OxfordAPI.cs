//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class OxfordAPI : AbstractAPI
//{

//    public static AbstractAPI Instance
//    {
//        get
//        {
//            //thread safe!
//            lock (_Lock)
//            {
//                if (_instance != null)
//                    return _instance;
//                OxfordAPI[] instances = FindObjectsOfType<OxfordAPI>();
//                //see if there are any already more instance of this
//                if (instances.Length > 0)
//                {
//                    //yay only 1 instance so give it back
//                    if (instances.Length == 1)
//                        return _instance = instances[0];

//                    //remove all other instance of it other than the 1st one
//                    for (int i = 1; i < instances.Length; i++)
//                        Destroy(instances[i]);
//                    return _instance = instances[0];
//                }

//                GameObject manage = new GameObject("OxfordAPI");
//                manage.AddComponent<OxfordAPI>();

//                return _instance = manage.GetComponent<OxfordAPI>();
//            }
//        }
//    }
//    void Awake()
//    {
//        Headers = new Dictionary<string, string>();

//        BaseURL = "https://od-api.oxforddictionaries.com/api/v1/";
//        Key = "54c3297ec769a7233b81ec5a1b68ac67";
//        App_Id = "34755312";

//        Headers["Accept"] = "application/json";
//        Headers["app_id"] = App_Id;
//        Headers["app_key"] = Key;
//    }

//    public override void SetValid(string word)
//    {
//        string newURL = BaseURL + "inflections/en/" + word;
//        InProgress = true;
//        StartCoroutine(Validity(newURL));
//    }

//    private IEnumerator Validity(string url)
//    {
//        using (WWW req = new WWW(url, null, Headers))
//        {
//            while (!req.isDone)
//                yield return req;

//            if (string.IsNullOrEmpty(req.error))
//            {
//                System.Text.StringBuilder headerBuilder = new System.Text.StringBuilder();
//                if (req.responseHeaders.Count > 0)
//                {
//                    string status = req.responseHeaders["STATUS"];
//                    if (status.Contains("200"))
//                        Valid = true;
//                }
//            }

//            InProgress = false;
//        }
//    }

//    public override void SetDefinition(string word)
//    {
//        throw new System.NotImplementedException();
//    }

//    public override void PrintAPIName()
//    {
//        Debug.Log("Oxford");
//    }

//    public override void SendWordToAPI(string word)
//    {
//        throw new System.NotImplementedException();
//    }
//}
