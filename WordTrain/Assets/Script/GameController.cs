using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    #region Singleton
    protected static GameController _instance;
    //Used only once to ensure when one thread have access to create the instance
    protected static readonly object _Lock = new object();

    public static GameController Instance
    {
        get
        {
            //thread safe!
            lock (_Lock)
            {
                if (_instance != null)
                    return _instance;
                GameController[] instances = FindObjectsOfType<GameController>();
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

                GameObject manage = new GameObject("GameController");
                manage.AddComponent<GameController>();

                return _instance = manage.GetComponent<GameController>();
            }
        }
    }
    #endregion

    AbstractAPI api;

    public GameObject playArea;

    public bool isDeckOut;

    private void Start()
    {
        api = WordsAPI.Instance;
        isDeckOut = false;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Validate();
        }

        if(isDeckOut)
        {
            //game over here;
        }
    }

    public void Validate()
    {
        string word = "";
        int potentialPoints = 0;
        foreach(Transform child in playArea.transform)
        {
            CardControl card = child.GetComponent<CardControl>();

            word += card.letter;
            potentialPoints += card.value;
        }

        if(!string.IsNullOrEmpty(word) && word.Length >= 3)
        {
            FillAPIData(word, potentialPoints);
        }

    }

    public void FillAPIData(string word, int potentialPoints)
    {
        api.Reset();
        api.SendWordToAPI(word);
        StartCoroutine(APICALLER(potentialPoints));
    }

    IEnumerator APICALLER(int potentialPoints)
    {
        while (api.InProgress)
        {
            yield return new WaitForSeconds(.3f);
        }

        //execute method based on the validity
        if (api.Valid)
        {
            Debug.LogFormat("The word has syllable(s)", api.Syllable);
            AddPoints(potentialPoints);
            LockLastCard();
            RemovePlayArea();
            AddCard();
        }
        else
        {
            Debug.LogFormat("The word is invalid");
        }

        TurnManager.Instance.NextTurn();
    }

    private void AddPoints(int points)
    {
        TurnManager turn = TurnManager.Instance;

        if(api.Syllable != 0)
            turn.CurrentPlayer.AddScore(points, api.Syllable);
    }

    private void LockLastCard()
    {
        CardControl lastCard = playArea.transform.GetChild(playArea.transform.childCount - 1).GetComponent<CardControl>();

        lastCard.isLock = true;
    }

    private void RemovePlayArea()
    {
        for(int i = playArea.transform.childCount - 2; i >= 0; i--)
        {
            DestroyImmediate(playArea.transform.GetChild(i).gameObject);
        }
    }

    private void AddCard()
    {
        if(!isDeckOut)
            TurnManager.Instance.CurrentPlayer.AddCard();
    }
}
