using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject hand;

    public int TotalPoint { get; private set; }
    public int EarnedPoint { get; private set; }
    //enable the cards in the hand to be draggable.
    public void UnlockHand()
    {
        if(hand != null)
        {
            foreach(Transform child in hand.transform)
            {
                CardControl cac = child.GetComponent<CardControl>();
                if (cac != null)
                    cac.isLock = false;
            }
        }
    }

    //disable the cards in the hands to be drag
    public void LockHand()
    {
        if (hand != null)
        {
            foreach (Transform child in hand.transform)
            {
                CardControl cac = child.GetComponent<CardControl>();
                if (cac != null)
                    cac.isLock = true;
            }
        }
    }

    public void AddScore(int points)
    {
        EarnedPoint = points;
        TotalPoint += points;
    }

}
