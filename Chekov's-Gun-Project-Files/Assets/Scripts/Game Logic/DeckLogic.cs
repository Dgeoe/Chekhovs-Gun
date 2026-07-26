using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class DeckLogic : MonoBehaviour
{
    //ON START: gun card is placed at a position in the deck 
    private int deck = 52;
    public int gunCard;
    public int currentDeckPos = 0; //itterate throughout the game
    public static DeckLogic instance;

    [Header("Card Types")]
    public List<CardTypes> cardTypes = new();
    [ReadOnly] public CardTypes Gun;
    [SerializeField] public List<CardTypes> currentDeck = new();
    private byte[] howMany;

    [Header("UI")]
    [SerializeField] private TextMeshPro GunCardPosDisplay;

    void Awake()
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
            if (howMany[choice] < cardTypes[choice].occurance)
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
        //increase deck by 1, move rest down, add gun
        Gun.cardName = "Gun";
        Gun.occurance = 1;
        Gun.ability = "Ends Game";

        //currentDeck[gunCard] = Gun;

        gunCard = Random.Range(0, currentDeck.Count + 1);
        currentDeck.Insert(gunCard, Gun);

        DisplayGunCardPos();
    }

    public void DisplayGunCardPos()
    {
        if (gunCard - currentDeckPos < 0) GunCardPosDisplay.gameObject.SetActive(false);
        GunCardPosDisplay.text = "" + (gunCard - currentDeckPos);
    }
}