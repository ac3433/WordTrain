using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    #region Singleton
    protected static RandomColor _instance;
    //Used only once to ensure when one thread have access to create the instance
    protected static readonly object _Lock = new object();

    public static RandomColor Instance
    {
        get
        {
            //thread safe!
            lock (_Lock)
            {
                if (_instance != null)
                    return _instance;
                RandomColor[] instances = FindObjectsOfType<RandomColor>();
                //see if there are any already more instance of this
                if (instances.Length > 0)
                {
                    //yay only 1 instance so give it back
                    if (instances.Length == 1)
                        return _instance = instances[0];

                    //remove all other instance of it other than the 1st one
                    for (int i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);
                    return _instance = instances[0];
                }

                GameObject manage = new GameObject("RandomColor");
                manage.AddComponent<RandomColor>();

                return _instance = manage.GetComponent<RandomColor>();
            }
        }
    }
    #endregion

    public GameObject[] baseTemplateCard;

    public GameObject GetRandomCard()
    {
        int rand = Random.Range(0, baseTemplateCard.Length - 1);
        return baseTemplateCard[rand];
    }
}
