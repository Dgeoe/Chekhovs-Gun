using System.Collections.Generic;
using UnityEngine;
using static DeckLogic;

public class PlayerHand : MonoBehaviour
{
    public bool yourTurn;
    [SerializeField] private List<CardTypes> currentHand = new();

    [Header("Card Designs")]
    [SerializeField] private Transform cardStartPos;
    [SerializeField] private GameObject cardPrefab;
    private Vector3 cardOffset = new Vector3((float)-0.24, (float)0.05, (float)0.09);

    public void TakeCard()
    {
        if (!yourTurn) return;

        currentHand.Add(DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);
        Instantiate(cardPrefab, cardStartPos.position + (cardOffset * currentHand.Count), cardStartPos.rotation, cardStartPos);
    }

    public void PlayCard()
    {

    }

    private void ScrollHand()
    {

    }

    public void EndTurn()
    {

    }

}
