using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLogic : MonoBehaviour
{
    //ON START: gun card is placed at a position in the deck 
    private int deck = 52;
    public int gunCard;
    public int currentDeckPos = 0; //itterate throughout the game
    public static DeckLogic instance;

    [Header("Card Types")]
    public List<CardTypes> cardTypes = new();
    [SerializeField] private List<CardTypes> currentDeck = new();
    private byte[] howMany;

    private void Awake()
    {
        if (instance == null) instance = this;

        howMany = new byte[cardTypes.Count];

        populateDeck();
    }

    private void populateDeck()
    {
        for (int j = 0; j < cardTypes.Count; j++)
        {
            howMany[j] = 0;
        }

        currentDeck.Clear();
        for (int k = 0; k < deck; k++)
        {
            //randomly select card from cardTypes
            int choice = UnityEngine.Random.Range(0, cardTypes.Count);

            //check occurance against how many are currently in list
            if (howMany[choice] <= cardTypes[choice].occurance)
            {
                //if good then assign to lowest unfilled position in currentDeck
                currentDeck.Add(cardTypes[choice]);
                howMany[choice]++;

            }
            else k--;

        }
    }

    void Start()
    {
        //increase deck by 1 add gun and move rest down 
        gunCard = UnityEngine.Random.Range(1, deck);
    }

    public void howFar()
    {
        int distance = gunCard - currentDeckPos;
        Debug.Log("Gun is at " + gunCard);
        Debug.Log("Which is " + distance + " Cards Down");
    }

    public void takeCard()
    {
        if (TurnCounter.instance.player1 == false) return;

        currentDeckPos++;
        if (currentDeckPos == gunCard)
        {
            Debug.Log("Win");
        }

        TurnCounter.instance.player1 = false;
        OpponentLogic.instance.EnemyTurn();
    }

    private bool isNull(CardTypes ct)
    {
        if (ct == null) return true;
        else return false;
    }

    [Serializable]
    public class CardTypes
    {
        [Header("Card Info")]
        public string cardName;
        public int occurance;
    }
}