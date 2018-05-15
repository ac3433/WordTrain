//using UnityEngine;
//using System.Collections;

//public class WordMuseAPI : AbstractAPI
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
//                WordMuseAPI[] instances = FindObjectsOfType<WordMuseAPI>();
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

//                GameObject manage = new GameObject("WordMuseAPI");
//                manage.AddComponent<WordMuseAPI>();

//                return _instance = manage.GetComponent<WordMuseAPI>();
//            }
//        }
//    }

//    private void Awake()
//    {
//        BaseURL = "https://api.datamuse.com/";
//    }

//    public override void SetDefinition(string word)
//    {
//        throw new System.NotImplementedException();
//    }

//    public override void SetValid(string word)
//    {
//        string newURL = string.Format("words?sp={0}",word);
//        InProgress = true;
//        StartCoroutine(Validity(newURL));
//    }

//    private IEnumerator Validity(string url)
//    {
//        using (WWW req = new WWW(url))
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

//    public override void PrintAPIName()
//    {
//        Debug.Log("Word Muse");
//    }

//    public override void SendWordToAPI(string word)
//    {
//        string newURL = string.Format("words?sp={0}&md=s&max=4", word);
//        InProgress = true;
//        StartCoroutine(Validity(newURL));
//    }

//    private IEnumerator SyllableAPI(string url, string word)
//    {
//        using (WWW req = new WWW(url))
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
//                    {
                        
//                    }
//                }
//            }

//            InProgress = false;
//        }
//    }
//}
