using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Card
{
    public Card(char letter, int value)
    {
        Letter = letter;
        Value = value;
    }
    public char Letter {get;set;}
    public int Value { get; set; }
}
