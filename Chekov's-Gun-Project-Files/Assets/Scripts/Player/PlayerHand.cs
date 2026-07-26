using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public bool yourTurn = false;
    private bool taken = false;
    private bool played = false;
    [SerializeField] private List<CardTypes> currentHand = new();

    [Header("Card Designs")]
    [SerializeField] private Transform cardStartPos;
    [SerializeField] private GameObject cardPrefab;
    private Vector3 cardOffset = new Vector3((float)-0.24, (float)0.05, (float)0.09);

    public void TakeCard()
    {
        if (!yourTurn || taken || DeckLogic.instance.currentDeck.Count == 0) return;

        currentHand.Add(DeckLogic.instance.currentDeck[0]);
        DeckLogic.instance.currentDeck.RemoveAt(0);

        GameObject g = Instantiate(cardPrefab, cardStartPos.position + (cardOffset * currentHand.Count), cardStartPos.rotation, cardStartPos);
        g.GetComponent<MeshRenderer>().material = currentHand[currentHand.Count - 1].texture;

        DeckLogic.instance.currentDeckPos++;
        UpdateDisplay();
        taken = true;
    }

    public void PlayCard()
    {
        if (!yourTurn || played || currentHand.Count == 0) return;

        Debug.Log(currentHand[0].ability);
        currentHand.RemoveAt(0);

        UpdateDisplay();
        played = true;
    }

    public void EndTurn()
    {
        TurnCounter.instance.checkTurn();
    }

    private void UpdateDisplay()
    {
        //Update GunCardPos
        DeckLogic.instance.DisplayGunCardPos();
    }

    public void NowsYourChance()
    {
        yourTurn = true;
        taken = false;
        played = false;
    }
}

