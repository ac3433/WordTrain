using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int TotalPoint { get; private set; }
    public int EarnedPoint { get; private set; }

    public Text txtScore;
    public Text txtMultiplier;
    public Text txtEarnedPoint;

    private void Start()
    {
        AddCard();
        if (TurnManager.Instance.CurrentPlayer != this)
        {
            LockHand();
        }

    }

    //enable the cards in the hand to be draggable.
    public void UnlockHand()
    {

        foreach (Transform child in transform)
        {
            CardControl cac = child.GetComponent<CardControl>();
            if (cac != null)
                cac.isLock = false;
        }
    }

    //disable the cards in the hands to be drag
    public void LockHand()
    {
        Debug.Log(gameObject.name);
        foreach (Transform child in transform)
        {
            CardControl cac = child.GetComponent<CardControl>();
            if (cac != null)
                cac.isLock = true;
        }
    }

    public void AddScore(int points, int multiplier)
    {

        EarnedPoint = points * multiplier;
        TotalPoint += points;

        txtEarnedPoint.text = EarnedPoint.ToString();
        txtMultiplier.text = "x" + multiplier.ToString();
        txtScore.text = TotalPoint.ToString();
    }

    public void AddCard()
    {
        int hand = transform.childCount;
        int countVowels = 0;
        int maxVowl = 2;
        foreach (Transform child in transform)
        {
            CardControl c = child.GetComponent<CardControl>();
            if (c.letter.ToLower().Equals("a") || c.letter.ToLower().Equals("e") || c.letter.ToLower().Equals("u") || c.letter.ToLower().Equals("i") || c.letter.ToLower().Equals("o"))
            {
                countVowels++;
            }
        }

        if (countVowels < 2)
        {
            maxVowl = 2;
        }

        for (int i = hand; i < 7; i++)
        {
            GameObject randomBlankCard = RandomColor.Instance.GetRandomCard();

            GameObject instantCard = Instantiate(randomBlankCard, transform);

            Card card;
            if (maxVowl != 0)
            {
                card = ResourceManager.Instance.GuarenteeVowels();
                maxVowl--;
            }
            else
                card = ResourceManager.Instance.Draw();


            if (card != null)
            {

                CardControl cc = instantCard.GetComponent<CardControl>();
                Text letter = instantCard.transform.GetChild(0).GetComponent<Text>();
                Text value = instantCard.transform.GetChild(1).GetComponent<Text>();

                cc.letter = card.Letter.ToString();
                cc.value = card.Value;

                letter.text = cc.letter;
                value.text = cc.value.ToString();
            }
            else
            {
                GameController.Instance.isDeckOut = true;
            }

        }

    }

    public void DiscardHand()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        AddCard();
    }
}
