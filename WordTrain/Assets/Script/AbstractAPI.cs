using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractAPI : MonoBehaviour {

    #region Singleton
    private static AbstractAPI _instance;
    //Used only once to ensure when one thread have access to create the instance
    private static readonly object _Lock = new object();

    public static AbstractAPI Instance
    {
        get
        {
            //thread safe!
            lock (_Lock)
            {
                if (_instance != null)
                    return _instance;
                AbstractAPI[] instances = FindObjectsOfType<AbstractAPI>();
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

                GameObject manage = new GameObject("AbstractAPI");
                manage.AddComponent<AbstractAPI>();

                return _instance = manage.GetComponent<AbstractAPI>();
            }
        }
    }
    #endregion

    protected string BaseURL { get; set; }
    protected string Key { get; set; }
    protected string App_Id { get; set; }
    protected Dictionary<string, string> Headers { get; set; }
    public bool InProgress { get; set; }
    public bool Valid { get; set; }

    public abstract void IsValid(string word);
    public abstract void GetDefinition(string word);

    public void Reset()
    {
        InProgress = false;
        Valid = false;
    }
}
