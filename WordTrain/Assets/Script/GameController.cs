using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public GameObject winner;
    public bool isDeckOut;
    public bool isEnd;

    private GameObject repeatedCard;
    private int repeats;

    private void Start()
    {
        api = WordsAPI.Instance;
        isDeckOut = false;
        isEnd = false;
        repeats = 0;
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
            if(api.Syllable == 0)
            {
                Invalid();
            }
            else
            {
                Debug.LogFormat("The word has syllable(s)", api.Syllable);
                AddPoints(potentialPoints);
                LockLastCard();
                RemovePlayArea();
                AddCard();
                repeatedCard = null;
                repeats = 0;
            }

        }
        else
        {
            Invalid();
            Debug.LogFormat("The word is invalid");
        }
        TurnManager.Instance.CurrentPlayer.isReset = false;
        if (isDeckOut && isEnd)
        {
            GameObject win = winner.transform.Find("WinnerName").gameObject;
            Text t = win.GetComponent<Text>();
            if (TurnManager.Instance.playerOne.TotalPoint == TurnManager.Instance.playerTwo.TotalPoint)
            {
                t.text = "Tie";
            }
            else if (TurnManager.Instance.playerTwo.TotalPoint > TurnManager.Instance.playerOne.TotalPoint)
            {
                t.text = "Player Two";
            }
            else
                t.text = "Player One";
            winner.SetActive(true);
        }
        else if (isDeckOut)
        {
            isEnd = true;
        }
        TurnManager.Instance.NextTurn();
    }

    private void Invalid()
    {
        List<GameObject> lst = new List<GameObject>();

        foreach (Transform child in playArea.transform)
        {
            CardControl cc = child.GetComponent<CardControl>();
            if (!cc.isLock)
            {
                lst.Add(child.gameObject);
            }
        }
        TurnManager.Instance.CurrentPlayer.UpdateDiscardValue(lst.Count);
        foreach (GameObject ob in lst)
        {
            DestroyImmediate(ob);
        }

        AddCard();

        if (playArea.transform.childCount != 0)
        {
            if (repeatedCard == null)
            {
                repeatedCard = playArea.transform.GetChild(0).gameObject;
                Debug.Log("hiyo");
            }
            else if (playArea.transform.GetChild(0).gameObject.Equals(repeatedCard))
            {
                repeats++;
                Debug.Log("hiya");
            }
            if (repeats == 1)
            {
                DestroyImmediate(repeatedCard);
                repeatedCard = null;
                repeats = 0;
            }
        }
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
        int count = playArea.transform.childCount - 1;
        for(int i = playArea.transform.childCount - 2; i >= 0; i--)
        {
            DestroyImmediate(playArea.transform.GetChild(i).gameObject);
        }



        TurnManager.Instance.CurrentPlayer.UpdateDiscardValue(count);
    }

    private void AddCard()
    {
        if(!isDeckOut)
            TurnManager.Instance.CurrentPlayer.AddCard();
    }
}
