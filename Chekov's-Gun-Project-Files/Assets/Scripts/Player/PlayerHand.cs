using System.Collections.Generic;
using UnityEngine;

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
        if (!yourTurn || DeckLogic.instance.currentDeck.Count == 0) return;

        currentHand.Add(DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);
        Instantiate(cardPrefab, cardStartPos.position + (cardOffset * currentHand.Count), cardStartPos.rotation, cardStartPos);

        UpdateDisplay();
    }

    public void PlayCard()
    {
        if (currentHand.Count == 0) return;

        Debug.Log(currentHand[0].ability);
        currentHand.RemoveAt(0);

        UpdateDisplay();
    }

    public void EndTurn()
    {

    }

    private void UpdateDisplay()
    {
        //Update GunCardPos
        DeckLogic.instance.currentDeckPos++;
        DeckLogic.instance.DisplayGunCardPos();
    }

}
