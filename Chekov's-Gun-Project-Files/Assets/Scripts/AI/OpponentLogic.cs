using System.Collections.Generic;
using UnityEngine;

public class OpponentLogic : MonoBehaviour
{
    [SerializeField] private List<CardTypes> currentHand = new();

    [Header("Card Designs")]
    [SerializeField] private Transform cardStartPos;
    [SerializeField] private GameObject cardPrefab;
    private Vector3 cardOffset = new Vector3((float)-0.24, (float)0.05, (float)0.09);

    public void EnemyTake()
    {
        if (DeckLogic.instance.currentDeck.Count == 0) return;

        currentHand.Add(DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);
        Instantiate(cardPrefab, cardStartPos.position + (cardOffset * currentHand.Count), cardStartPos.rotation, cardStartPos);

        DeckLogic.instance.currentDeckPos++;
        UpdateDisplay();
        TurnCounter.instance.checkTurn();
    }

    private void UpdateDisplay()
    {
        //Update GunCardPos
        DeckLogic.instance.DisplayGunCardPos();
    }
}
