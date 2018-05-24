using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    #region Singleton
    protected static ResourceManager _instance;
    //Used only once to ensure when one thread have access to create the instance
    protected static readonly object _Lock = new object();

    public static ResourceManager Instance
    {
        get
        {
            //thread safe!
            lock (_Lock)
            {
                if (_instance != null)
                    return _instance;
                ResourceManager[] instances = FindObjectsOfType<ResourceManager>();
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

                GameObject manage = new GameObject("ResourceManager");
                manage.AddComponent<ResourceManager>();

                return _instance = manage.GetComponent<ResourceManager>();
            }
        }
    }
    #endregion

    private Dictionary<Card, int> deck; //Card, size of cards available
    private Dictionary<Card, int> startingDeck;
    private List<Card> listing;
    private List<Card> vowels;
    private void Awake()
    {
        deck = new Dictionary<Card, int>();
        listing = new List<Card>();
        vowels = new List<Card>();
        deck.Add(new Card('A', 1), 16); //0
        deck.Add(new Card('B', 3), 4);  //1
        deck.Add(new Card('C', 3), 6);
        deck.Add(new Card('D', 2), 8);  //3
        deck.Add(new Card('E', 1), 24);
        deck.Add(new Card('F', 4), 4);  //5
        deck.Add(new Card('G', 2), 5);
        deck.Add(new Card('H', 4), 5);  //7
        deck.Add(new Card('I', 1), 13);
        deck.Add(new Card('J', 8), 2);  //9
        deck.Add(new Card('K', 5), 2);
        deck.Add(new Card('L', 5), 2); //11
        deck.Add(new Card('M', 3), 6);  //12
        deck.Add(new Card('N', 1), 13);
        deck.Add(new Card('O', 1), 15); //14
        deck.Add(new Card('P', 3), 4);
        deck.Add(new Card('Q', 10), 2); //16
        deck.Add(new Card('R', 1), 13);
        deck.Add(new Card('S', 1), 10); //18
        deck.Add(new Card('T', 1), 15);
        deck.Add(new Card('U', 1), 7);  //20
        deck.Add(new Card('V', 4), 3);
        deck.Add(new Card('W', 4), 4);  //22
        deck.Add(new Card('X', 8), 2);
        deck.Add(new Card('Y', 4), 4);  //24
        deck.Add(new Card('Z', 10), 2);

        foreach (Card card in deck.Keys)
        {
            listing.Add(card);

        }

        vowels.Add(new Card('A', 1));
        vowels.Add(new Card('E', 1));
        vowels.Add(new Card('I', 1));
        vowels.Add(new Card('U', 1));
        vowels.Add(new Card('O', 1));


    }

    /// <summary>
    /// Draw a card from the deck
    /// </summary>
    /// <returns>Null = the deck is empty or return the card</returns>
    public Card Draw()
    {
        if (listing.Count == 0)
            return null;
        int random = Random.Range(0, listing.Count - 1);



        Card card = listing[random];
        RemoveCard(card);
        return card;

    }

    public Card GuarenteeVowels()
    {
        int rand = Random.Range(0, vowels.Count - 1);
        return vowels[rand];
    }

    public void RemoveCard(Card card)
    {
        if (deck[card]-- <= 0)
            listing.Remove(card);
    }

    public void Reset()
    {
        listing = new List<Card>();
        foreach (Card card in deck.Keys)
        {
            listing.Add(card);
        }

        deck = Clone.DeepClone<Dictionary<Card, int>>(startingDeck);

    }
}
