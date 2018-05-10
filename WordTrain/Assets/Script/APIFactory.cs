using UnityEngine;
using System.Collections;

public class APIFactory
{
    public static AbstractAPI API(string type = "")
    {
        switch(type.ToLower())
        {
            case "oxford":
                return OxfordAPI.Instance;
            case "wordmuse":
                return WordMuseAPI.Instance;
            default:
                return OxfordAPI.Instance;
        }
    }
}
